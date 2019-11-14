using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaApp.Models
{
    public struct RoomConfig
    {
        public const int RoomWidth = 18;
        public const int RoomHeight = 12;
        public const int MaxTickets = 10;
    }

    public class ReservationFlowViewModel
    {
        public Showing Showing { get; set; }
        public Movie Movie { get; set; }

        public List<PlacePosition> SelectedPlaces { get; set; }
        public List<PlacePosition> TakenPlaces { get; set; }

        public string PostUrl { get; set; }

        public int RoomWidth  { get { return RoomConfig.RoomWidth; } }
        public int RoomHeight { get { return RoomConfig.RoomHeight; } }
        public int MaxTickets { get { return RoomConfig.MaxTickets;  } }
    }
}