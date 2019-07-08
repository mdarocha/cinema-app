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

        // GET: ReservationFlow
        [Authorize]
        public ActionResult Index(int id)
        {
            var showing = storage.Showings.Include("Movie").Single(s => s.ID == id);
            return View(showing);
        }
    }
}