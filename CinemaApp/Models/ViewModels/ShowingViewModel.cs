using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaApp.Models
{
    public class ShowingViewModel
    {
        public Movie Movie { get; set; }
        public List<Showing> Showings { get; set; }
    }
}