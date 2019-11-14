using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CinemaApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using CinemaApp.DAL;
using System.Net.Mail;

namespace CinemaApp.Controllers
{
    public class ReservationFlowController : Controller
    {
        private IUnitOfWork db;
        private IMailSender mail;
        private Func<string> GetUserId;

        public ReservationFlowController()
        {
            db = new UnitOfWork();
            mail = new MailSender();
            GetUserId = () => User.Identity.GetUserId();
        }

        public ReservationFlowController(IUnitOfWork db, IMailSender mail, Func<string> idFunc)
        {
            this.db = db;
            this.mail = mail;
            GetUserId = idFunc;
        }

        // GET: ReservationFlow/id
        [Authorize]
        public ActionResult Index(int? id)
        {
            if (!id.HasValue)
                return View("Error");

            Showing showing = db.Repo<Showing>().Get(id.Value);

            if (showing.Time < DateTime.Now)
            {
                return View("Error");
            }

            var takenPlaces = (db.Repo<Place>() as IPlacesRepo).GetTakenPlaces(showing.ID);

            var movie = new Movie
            {
                Title = showing.Movie.Title,
                Description = showing.Movie.Description,
                Runtime = showing.Movie.Runtime,
                PosterUrl = Url.Content(showing.Movie.PosterUrl),
            };

            showing.Movie = null;

            var viewModel = new ReservationFlowViewModel
            {
                Showing = showing,
                Movie = movie,

                SelectedPlaces = new List<PlacePosition>(),
                TakenPlaces = takenPlaces,

                PostUrl = Url.Action("MakeReservation"),
            };

            return View(viewModel);
        }

        // POST: ReservationFlow/MakeReservation
        [Authorize]
        [HttpPost]
        public ActionResult MakeReservation(ReservationFlowViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false });
            }

            var showing = db.Repo<Showing>().Get(model.Showing.ID);
            if (showing.Time < DateTime.Now)
            {
                return Json(new { success = false });
            }

            var takenPlaces = (db.Repo<Place>() as IPlacesRepo).GetTakenPlaces(model.Showing.ID);

            if (takenPlaces.Intersect(model.SelectedPlaces, new PlacePosition.Comparer()).Count() != 0)
            {
                return Json(new { success = false });
            }

            var reservation = new Reservation
            {
                ShowingID = model.Showing.ID,
                CinemaUserID = User.Identity.GetUserId(),

                Places = model.SelectedPlaces.ConvertAll(p => new Place { x = p.x, y = p.y }),
                ReservationDate = DateTime.Now
            };

            db.Repo<Reservation>().Add(reservation);
            db.Save();

            mail.SendMail(User.Identity.Name, "Potwierdzenie rezerwacji",
                string.Format("Dziękujemy za rezerwacje filmu {0}, {1}. Seans odbędzie się o {2}. Nr rezerwacji {3}",
                    showing.Movie.Title,
                    showing.GetMetadataString(),
                    showing.Time.ToString("HH:mm, dddd dd.MM"),
                    reservation.ID));

            return Json(new { success = true, redirect_url = Url.Action("Index", "Manage") });
        }

        [Authorize]
        [HttpPost]
        public ActionResult CancelReservation(int id)
        {
            var reservation = db.Repo<Reservation>().Get(id);

            if(reservation.CinemaUserID != GetUserId())
            {
                return Json(new { success = false });
            }

            if(DateTime.Now > reservation.Showing.Time.AddMinutes(-30))
            {
                return Json(new { success = false });
            }

            db.Repo<Reservation>().Remove(reservation);
            db.Save();

            mail.SendMail(User.Identity.Name, "Anulowanie rezerwacji",
                string.Format("Anulowano rezerwację nr {0}",
                reservation.ID));

            return Json(new { success = true });
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public ActionResult CancelReservationAdmin(int id)
        {
            var reservation = db.Repo<Reservation>().Get(id);

            mail.SendMail(reservation.CinemaUser.Email, "Anulowanie rezerwacji",
                string.Format("Twoja rezerwacja nr {0} została anulowana przez administratora", reservation.ID));

            db.Repo<Reservation>().Remove(reservation);
            db.Save();

            return Json(new { success = true });
        }
    }
}