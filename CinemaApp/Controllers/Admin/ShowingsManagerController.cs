using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CinemaApp.DAL;
using CinemaApp.Models;

namespace CinemaApp.Controllers
{
    public class ShowingsManagerController : Controller
    {
        private IUnitOfWork db;
        private IMailSender mail;

        public ShowingsManagerController()
        {
            db = new UnitOfWork();
            mail = new MailSender();
        }

        public ShowingsManagerController(IUnitOfWork db, IMailSender mail)
        {
            this.db = db;
            this.mail = mail;
        }

        // GET: ShowingsAdmin
        public ActionResult Index()
        {
            var showings = db.Repo<Showing>().GetList().OrderBy(s => -s.Time.Ticks);
            return View(showings);
        }

        // GET: ShowingsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Showing showing = db.Repo<Showing>().Get(id.Value);
            if (showing == null)
            {
                return HttpNotFound();
            }

            var viewModel = new ShowingDetailsViewModel
            {
                Showing = showing,
                Reservations = (db.Repo<Reservation>() as IReservationsRepo).GetReservationsForShowing(showing),
                MovieEndTime = default(DateTime).Add(showing.Time.TimeOfDay).AddMinutes(showing.Movie.Runtime + showing.AfterShowingCleaningTime),
            };

            return View(viewModel);
        }

        // GET: ShowingsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.MovieID = new SelectList(db.Repo<Movie>().GetList(), 
                "ID", "Title");
            return View();
        }

        // POST: ShowingsAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MovieID,Time,Is2D,IsSubtitles")] Showing showing)
        {
            ViewBag.MovieID = new SelectList(db.Repo<Movie>().GetList(), 
                "ID", "Title", showing.MovieID);

            if (ModelState.IsValid)
            {
                showing.Movie = db.Repo<Movie>().Get(showing.MovieID);

                var startTime = default(DateTime).AddHours(8);
                var endTime = default(DateTime).AddHours(23);
                var showingTime = default(DateTime).Add(showing.Time.TimeOfDay);


                var movieEndtime = default(DateTime).Add(showingTime.TimeOfDay).
                    AddMinutes(showing.Movie.Runtime + showing.AfterShowingCleaningTime);

                if(showingTime < startTime)
                {
                    ModelState.AddModelError("", "Seans za wcześnie");
                    return View(showing);
                }

                if(movieEndtime > endTime)
                {
                    ModelState.AddModelError("", string.Format("Seans konczy sie za późno (o {0})", movieEndtime.ToString("HH:mm")));
                    return View(showing);
                }

                var showingsInDay = (db.Repo<Showing>() as IShowingsRepo).GetShowingsForDay(showing.Time);
                var overlapping = showingsInDay
                    .Where(s =>
                        (s.Time >= showing.Time && s.Time <= showing.EndTime) ||
                        (s.EndTime >= showing.Time && s.EndTime <= showing.EndTime))
                    .ToList();

                if(overlapping.Count != 0)
                {
                   foreach(var overlap in overlapping) {
                        ModelState.AddModelError("", string.Format("Seans {0}-{1} koliduje z tym seansem", overlap.Time.ToString("HH:mm"), overlap.EndTime.ToString("HH:mm")));
                   }

                    return View(showing);
                }

                db.Repo<Showing>().Add(showing);
                db.Save();
                return RedirectToAction("Index");
            }

            return View(showing);
        }

        // GET: ShowingsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Showing showing = db.Repo<Showing>().Get(id.Value);
            if (showing == null)
            {
                return HttpNotFound();
            }

            return View(showing);
        }

        // POST: ShowingsAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Time,Is2D,IsSubtitles")] Showing showing)
        {
            if (ModelState.IsValid)
            {
                var oldShowing = db.Repo<Showing>().Get(showing.ID);

                oldShowing.Time = showing.Time;
                oldShowing.Is2D = showing.Is2D;
                oldShowing.IsSubtitles = showing.IsSubtitles;

                db.Repo<Showing>().Update(oldShowing);
                db.Save();

                SendMailToReservations(oldShowing, "Zmiana seansu", "Posiadasz rezerwację na seans, który został zmieniony. Wejdź na stronę, aby zobaczyć zmiany");

                return RedirectToAction("Index");
            }

            return View(showing);
        }

        // GET: ShowingsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Showing showing = db.Repo<Showing>().Get(id.Value);
            if (showing == null)
            {
                return HttpNotFound();
            }

            return View(showing);
        }

        // POST: ShowingsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Showing showing = db.Repo<Showing>().Get(id);

            SendMailToReservations(showing, "Anulowanie seansu",
                string.Format("Seans filmu {0}, {1}, o {2} został anulowany. Twoja rezerwacja została anulowana",
                        showing.Movie.Title,
                        showing.GetMetadataString(),
                        showing.Time.ToString("HH:mm, dd.MM")));

            db.Repo<Showing>().Remove(showing);
            db.Save();
            return RedirectToAction("Index");
        }

        private void SendMailToReservations(Showing showing, string subject, string body)
        {
            List<Reservation> reservations = (db.Repo<Reservation>() as IReservationsRepo).GetReservationsForShowing(showing);
            foreach(var reservation in reservations)
            {
                mail.SendMail(reservation.CinemaUser.Email, subject, body);
            }
        }

        private bool VerifyShowingTime(Showing showing)
        {

            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
