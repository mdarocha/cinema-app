using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CinemaApp.Models;

namespace CinemaApp.DAL
{
    public interface IPlacesRepo : IGenericRepo<Place>
    {
        List<PlacePosition> GetTakenPlaces(int ShowingId);
    }

    public class PlacesRepo : GenericRepo<Place>, IPlacesRepo
    {
        public PlacesRepo(CinemaDbContext context) : base(context)
        {
        }

        public List<PlacePosition> GetTakenPlaces(int ShowingId)
        {
            return set.Where(p => p.Reservation.ShowingID == ShowingId)
                .ToList()
                .ConvertAll(p => new PlacePosition { x = p.x, y = p.y });
        }
    }

    public interface IReservationsRepo : IGenericRepo<Reservation>
    {
        List<Reservation> GetReservationsForUser(CinemaUser user);
        List<Reservation> GetReservationsForShowing(Showing showing);
        List<Reservation> GetReservationsForMovie(Movie movie);
    }

    public class ReservationsRepo : GenericRepo<Reservation>, IReservationsRepo
    {
        public ReservationsRepo(CinemaDbContext context) : base(context)
        {

        }

        public override Reservation Get(int id)
        {
            return set.Include("CinemaUser").Single(r => r.ID == id);
        }

        public List<Reservation> GetReservationsForUser(CinemaUser user)
        {
            return set.Where(r => r.CinemaUserID == user.Id).OrderByDescending(r => r.ReservationDate).ToList();
        }

        public List<Reservation> GetReservationsForShowing(Showing showing)
        {
            return set.Include("CinemaUser").Where(r => r.ShowingID == showing.ID).ToList();
        }

        public List<Reservation> GetReservationsForMovie(Movie movie)
        {
            return set.Include("CinemaUser").Include("Showing").Where(r => r.Showing.MovieID == movie.ID).ToList();
        }
    }

    public interface IShowingsRepo : IGenericRepo<Showing>
    {
        List<List<Showing>> GetShowingsByMovie(DateTime day);
        List<Showing> GetShowingsForDay(DateTime day);
    }

    public class ShowingsRepo : GenericRepo<Showing>, IShowingsRepo
    {
        public ShowingsRepo(CinemaDbContext context) : base(context)
        {

        }

        public List<List<Showing>> GetShowingsByMovie(DateTime day)
        {
            return set.Where(s => s.Time.Year == day.Year && 
                             s.Time.Month == day.Month && 
                             s.Time.Day == day.Day)
                .GroupBy(s => s.Movie).Select(g => g.OrderBy(s => (s.Time.Hour * 60) + s.Time.Minute).ToList()).ToList();
        }

        public List<Showing> GetShowingsForDay(DateTime day)
        {
            return set.Where(s => s.Time.Year == day.Year &&
                            s.Time.Month == day.Month &&
                            s.Time.Day == day.Day).ToList();
        }

        public override IEnumerable<Showing> GetList()
        {
            return set.Include("Movie").AsEnumerable();
        }
    }
}