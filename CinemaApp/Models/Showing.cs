using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaApp.Models
{
    public class Showing
    {
        public int ID { get; set; }

        public int MovieID { get; set; }
        public virtual Movie Movie { get; set; }

        public DateTime Time { get; set; }
    }
}