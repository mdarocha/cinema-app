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
    }
}