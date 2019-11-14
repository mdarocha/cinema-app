using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CinemaApp.Models;
using CinemaApp.DAL;

namespace CinemaApp.Controllers
{
    public class MoviesController : Controller
    {
        IUnitOfWork db;

        public MoviesController()
        {
            db = new UnitOfWork();
        }

        public MoviesController(IUnitOfWork db)
        {
            this.db = new UnitOfWork();
        }

        // GET: Movies
        public ActionResult Index()
        {
            return View();
        }

        // GET: List?day=
        public ActionResult List(int day = 0)
        {
            var date = DateTime.Now.Date.AddDays(day);
            var showingsByMovie = (db.Repo<Showing>() as IShowingsRepo).GetShowingsByMovie(date);

            List<ShowingViewModel> list_movies = new List<ShowingViewModel>();
            foreach(var showings in showingsByMovie)
            {
                list_movies.Add(new ShowingViewModel()
                {
                    Movie = showings[0].Movie,
                    Showings = showings,
                });
            }

            return PartialView("ShowingListPartial", list_movies);
        }
    }
}