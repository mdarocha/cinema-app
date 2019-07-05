using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Models
{
    public class Movie
    {
        [Required]
        public string Title { get; set; }

        public decimal Price { get; set; }
    }
}