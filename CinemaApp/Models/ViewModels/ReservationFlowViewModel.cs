using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaApp.Models
{
    public class ReservationFlowViewModel
    {
        public Showing Showing { get; set; }
        public List<PlacePosition> TakenPlaces { get; set; }

        public string PostUrl { get; set; }

        public int RoomWidth { get { return 18; } }
        public int RoomHeight { get { return 12; } }
        public int MaxTickets { get { return 10;  } }
    }
}