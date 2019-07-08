using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CinemaApp.Models;

namespace CinemaApp.Controllers
{
    public class MoviesController : Controller
    {
        CinemaEntities storage = new CinemaEntities();

        // GET: Movies
        public ActionResult Index()
        {
            return View();
        }

        // GET: List?day=
        public ActionResult List(int day = 0)
        {
            try
            {
                var date = DateTime.Now.Date.AddDays(day);

                var showings = storage.Showings.Include("Movie")
                        .Where(s => s.Time.Year == date.Year && s.Time.Month == date.Month && s.Time.Day == date.Day);
                return PartialView("ShowingListPartial", showings);
            } catch
            {
                return View("Error");
            }
        }
    }
}