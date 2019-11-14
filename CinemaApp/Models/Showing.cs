using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using CinemaApp.Resources;

namespace CinemaApp.Models
{
    public class Showing : IGenericModel
    {
        public int ID { get; set; }

        public int MovieID { get; set; }
        public virtual Movie Movie { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name ="Czas")]
        public DateTime Time { get; set; }

        public bool Is2D { get; set; }
        public bool IsSubtitles { get; set; }

        public string GetMetadataString()
        {
            return string.Format("{0}, {1}",
                (IsSubtitles ? Strings.Subtitles : Strings.Dubbing),
                (Is2D ? Strings.Movie2D : Strings.Movie3D));
        }

        public DateTime EndTime { get { return (Movie != null) ? Time.AddMinutes(Movie.Runtime + AfterShowingCleaningTime) : default(DateTime);  } }
        public int AfterShowingCleaningTime { get { return 15; } }
    }
}