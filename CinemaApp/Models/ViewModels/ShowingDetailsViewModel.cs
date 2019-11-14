using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaApp.Models
{
    public class ShowingDetailsViewModel
    {
        public Showing Showing { get; set; }
        public DateTime MovieEndTime { get; set; }

        public IEnumerable<Reservation> Reservations { get; set; }
    }
}