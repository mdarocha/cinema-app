using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaApp.Models
{
    public class PlaceSelectorViewModel
    {
        public Showing Showing { get; set; }
        public List<Place> TakenPlaces { get; set; }

        public int RoomWidth { get { return 18; } }
        public int RoomHeight { get { return 12; } }
        public int MaxTickets { get { return 10;  } }
    }
}