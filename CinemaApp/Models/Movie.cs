using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Models
{
    public class Movie : IGenericModel
    {
        public int ID { get; set; }

        [Required]
        [Display(Name ="Tytuł")]
        public string Title { get; set; }

        [Required]
        [Display(Name ="Opis")]
        public string Description { get; set; }        

        [Display(Name ="Czas trwania (min)")]
        [Range(1, 600)]
        public int Runtime { get; set; }

        [Display(Name ="Plakat")]
        public string PosterUrl { get; set; }
    }
}