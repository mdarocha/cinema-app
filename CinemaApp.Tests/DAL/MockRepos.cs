using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CinemaApp.DAL;
using CinemaApp.Models;

namespace CinemaApp.Tests.DAL
{
    class MockGenericRepo<T> : IGenericRepo<T> where T: class, IGenericModel, new()
    {
        public void Add(T e)
        {
        }

        public virtual T Get(int id)
        {
            return new T();
        }

        public T Get(Expression<Func<T, bool>> p)
        {
            return new T();
        }

        public IEnumerable<T> GetList()
        {
            return new List<T> { new T() };
        }

        public IEnumerable<T> GetList(Expression<Func<T, bool>> p)
        {
            return GetList(); 
        }

        public void Remove(T e)
        {
        }

        public void Update(T e)
        {
        }
    }

    class MockReservationRepo : MockGenericRepo<Reservation>, IReservationsRepo
    {
        public Reservation Reservation { get; set; }

        public MockReservationRepo()
        {
            Reservation = new Reservation { Showing = new MockShowingsRepo().Get(0), CinemaUserID = "testId" };
        }

        public List<Reservation> GetReservationsForUser(CinemaUser user)
        {
            throw new NotImplementedException();
        }

        public override Reservation Get(int id)
        {
            return Reservation;
        }

        public List<Reservation> GetReservationsForShowing(Showing showing)
        {
            throw new NotImplementedException();
        }

        public List<Reservation> GetReservationsForMovie(Movie movie)
        {
            throw new NotImplementedException();
        }
    }

    class MockPlacesRepo : MockGenericRepo<Place>, IPlacesRepo
    {
        public List<PlacePosition> GetTakenPlaces(int ShowingId)
        {
            return new List<PlacePosition> { new PlacePosition { x = 0, y = 0 }, new PlacePosition { x = 1, y = 0 } };
        }
    }

    class MockShowingsRepo : MockGenericRepo<Showing>, IShowingsRepo
    {
        public Showing Showing { get; set; }

        public MockShowingsRepo()
        {
            Showing = new Showing { Time = DateTime.Now.AddHours(1), Movie = new Movie() };
        }

        public List<List<Showing>> GetShowingsByMovie(DateTime day)
        {
            return new List<List<Showing>> { new List<Showing> { Showing } };
        }

        public override Showing Get(int id)
        {
            return Showing;
        }

        public List<Showing> GetShowingsForDay(DateTime day)
        {
            throw new NotImplementedException();
        }
    }
}
