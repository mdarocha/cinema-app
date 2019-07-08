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
        CinemaDbContext storage = new CinemaDbContext();

        // GET: Movies
        public ActionResult Index()
        {
            return View();
        }

        // GET: List?day=
        public ActionResult List(int day = 0)
        {
//            try
//            {
                var date = DateTime.Now.Date.AddDays(day);

                var movies = storage.Showings.Include("Movie")
                    .Where(s => s.Time.Year == date.Year && s.Time.Month == date.Month && s.Time.Day == date.Day)
                    .GroupBy(s => s.Movie).Select(g => g.ToList()).ToList();

                List<ShowingViewModel> list_movies = new List<ShowingViewModel>();
                foreach(var movie in movies)
                {
                    list_movies.Add(new ShowingViewModel()
                    {
                        Movie = movie[0].Movie,
                        Showings = movie,
                    });
                }

                return PartialView("ShowingListPartial", list_movies);

/*            } catch
            {
                return View("Error");
            }*/
        }
    }
}