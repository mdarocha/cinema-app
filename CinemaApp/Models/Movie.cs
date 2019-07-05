using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Models
{
    public class Movie
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }        
        public int Runtime { get; set; }
        public string PosterUrl { get; set; }
    }
}