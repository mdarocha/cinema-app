using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CinemaApp.Models
{
    public class Reservation
    {
        public int ID { get; set; }

        public int ShowingID { get; set; }
        public virtual Showing Showing { get; set; }

        public string CinemaUserID { get; set; }
        public virtual CinemaUser CinemaUser { get; set; }

        public DateTime ReservationDate { get; set; }

        public virtual List<Place> Places { get; set; }
    }
}