using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaApp.Models
{
    public class PlacePosition
    {
        public int x { get; set; }
        public int y { get; set; }

        public static string PlaceName(PlacePosition pos)
        {
            return (((char)(97 + pos.y)).ToString() + (pos.x + 1)).ToString().ToUpper();
        }

        public static string PlaceName(Place pos)
        {
            return PlaceName(new PlacePosition { x = pos.x, y = pos.y });
        }

        public static List<PlacePosition> FromPlaces(List<Place> pos)
        {
            return pos.ConvertAll(p => new PlacePosition { x = p.x, y = p.y });
        }

        public class Comparer : IEqualityComparer<PlacePosition>
        {
            public bool Equals(PlacePosition a, PlacePosition b)
            {
                return (a.x == b.x) && (a.y == b.y);
            }

            public int GetHashCode(PlacePosition obj)
            {
                return obj.x * RoomConfig.RoomWidth + obj.y;
            }
        }
    }
}