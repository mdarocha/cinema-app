using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaApp.Models
{
    public class Place
    {
        public int ID { get; set; }
        
        public int ReservationID { get; set; }
        public virtual Reservation Reservation { get; set; }

        public int x { get; set; }
        public int y { get; set; }
    }
}