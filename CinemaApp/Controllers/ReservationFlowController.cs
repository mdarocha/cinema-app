using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CinemaApp.DAL;
using CinemaApp.Models;

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


            var viewModel = new ReservationFlowViewModel
            {
                Showing = showing,
                TakenPlaces = new List<Place> { },

                PostUrl = Url.Action("MakeReservation")
            };

            return View(viewModel);
        }

        // POST: ReservationFlow/MakeReservation
        [Authorize]
        [HttpPost]
        public ActionResult MakeReservation(ReservationFlowViewModel model, List<Place> places)
        {
            return Json(new { model, places });
        }
    }
}