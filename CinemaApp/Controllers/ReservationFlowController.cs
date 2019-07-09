using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CinemaApp.DAL;
using CinemaApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace CinemaApp.Controllers
{
    public class ReservationFlowController : Controller
    {
        CinemaDbContext storage = new CinemaDbContext();

        // GET: ReservationFlow/id
        [Authorize]
        public ActionResult Index(int? id)
        {
            if (!id.HasValue)
                return View("Error");

            Showing showing;
            try
            {
                showing = storage.Showings.Include("Movie").Single(s => s.ID == id);
            } catch (Exception e)
            {
                return View("Error");
            }

            List<PlacePosition> takenPlaces = storage.Places.Include("Reservation")
                .Where(p => p.Reservation.ShowingID == id).ToList().ConvertAll(p => new PlacePosition { x = p.x, y = p.y });

            var viewModel = new ReservationFlowViewModel
            {
                Showing = showing,
                TakenPlaces = takenPlaces,

                PostUrl = Url.Action("MakeReservation")
            };

            return View(viewModel);
        }

        // POST: ReservationFlow/MakeReservation
        [Authorize]
        [HttpPost]
        public ActionResult MakeReservation(ReservationFlowViewModel model, List<PlacePosition> places)
        {
            //TODO add server side validation of data
            var reservation = new Reservation
            {
                CinemaUserID = User.Identity.GetUserId(),
                ShowingID = model.Showing.ID,
                Places = places.ConvertAll(p => new Place { x = p.x, y = p.y}),
                ReservationDate = DateTime.Now
            };

            storage.Reservations.Add(reservation);
            storage.SaveChanges();

            return Json(new { success = true });
        }
    }
}