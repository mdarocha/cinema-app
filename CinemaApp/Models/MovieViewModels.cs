using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaApp.Models
{
    public class MovieListViewModel
    {
        public IEnumerable<Movie> movies { get; set; }
        public IEnumerable<DateTime> days { get; set; }
    }
}