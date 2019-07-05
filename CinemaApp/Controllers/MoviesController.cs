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
            var days = new List<DateTime>();

            var today = DateTime.Now.Date;
            days.Add(today);

            for(int i = 0; i < 5; i++)
            {
                today = today.AddDays(1);
                days.Add(today);
            }

            var movies = storage.Movies.ToList();
            var viewModel = new MovieListViewModel
            {
                movies = movies,
                days = days
            };
            return View(viewModel);
        }
    }
}