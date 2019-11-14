using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using CinemaApp.Models;
using CinemaApp.DAL;

namespace CinemaApp.Controllers
{
    [Authorize(Roles ="Admin")]
    public class MoviesManagerController : Controller
    {
        #region Constructors
        private IUnitOfWork db;
        private IMailSender mail;

        public MoviesManagerController()
        {
            db = new UnitOfWork();
            mail = new MailSender();
        }

        public MoviesManagerController(IUnitOfWork db, IMailSender mail)
        {
            this.db = db;
            this.mail = mail;
        }
        #endregion

        // GET: MoviesManager
        public ActionResult Index()
        {
            return View(db.Repo<Movie>().GetList());
        }

        // GET: MoviesManager/Create
        public ActionResult Create()
        {
            return View();
        }

        private List<string> AllowedContentTypes = new List<String>
        {
            "image/bmp",
            "image/jpeg",
            "image/png",
        };

        // POST: MoviesManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Description,Runtime")] Movie movie, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if(file == null)
                {
                    ModelState.AddModelError("", "Dodaj plakat");
                    return View(movie);
                }

                if(!AllowedContentTypes.Contains(file.ContentType))
                {
                    ModelState.AddModelError("", "Nieprawidłowy plik");
                    return View(movie);
                }

                Image image = Image.FromStream(file.InputStream, true, true);

                movie.PosterUrl = ScaleAndSavePoster(image);

                db.Repo<Movie>().Add(movie);
                db.Save();

                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: MoviesManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Repo<Movie>().Get(id.Value);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: MoviesManager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,Runtime,PosterUrl")] Movie movie, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if(file != null)
                {
                    if (!AllowedContentTypes.Contains(file.ContentType)) {
                        ModelState.AddModelError("", "Nieprawidłowy plik");
                        return View(movie);
                    }
                    else
                    {
                        DeletePoster(movie.PosterUrl);
                        Image image = Image.FromStream(file.InputStream);
                        movie.PosterUrl = ScaleAndSavePoster(image);
                    }
                }

                db.Repo<Movie>().Update(movie);
                db.Save();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: MoviesManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Repo<Movie>().Get(id.Value);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: MoviesManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Repo<Movie>().Get(id);

            SendCancelledReservationMail(movie);
            DeletePoster(movie.PosterUrl);

            db.Repo<Movie>().Remove(movie);
            db.Save();
            return RedirectToAction("Index");
        }

        private void DeletePoster(string posterUrl)
        {
            var path = Server.MapPath(posterUrl);
            System.IO.File.Delete(path);
        }

        private void SendCancelledReservationMail(Movie movie)
        {
            List<Reservation> reservations = (db.Repo<Reservation>() as IReservationsRepo).GetReservationsForMovie(movie);
            foreach(var reservation in reservations)
            {
                mail.SendMail(reservation.CinemaUser.Email, "Anulowanie rezerwacji", string.Format(
                    "Twoje rezerwacje na film {0} zostały anulowane",
                    movie.Title));
            }
        }

        private string ScaleAndSavePoster(Image image)
        {
            //resize to keep constant aspect ratio (27 : 40) of a movie poster
            int width = (image.Width < 2500) ? image.Width : 2500;
            int height = (int) Math.Floor((40.0 / 27.0) * width);

            Bitmap bitmap = new Bitmap(image.Width, height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, 
                    new Rectangle(0, 0, image.Width, height),
                    new Rectangle(0, 0, image.Width, image.Height),
                    GraphicsUnit.Pixel);
            }

            var filename = System.Guid.NewGuid().ToString() + ".png";
            var path = Path.Combine("~/Images/Uploads", filename);

            bitmap.Save(Server.MapPath(path), ImageFormat.Png);
            return path; 
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
