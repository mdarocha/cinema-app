using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaApp.DAL;
using CinemaApp.Models;

namespace CinemaApp.Tests.DAL
{
    class MockUnitOfWork : IUnitOfWork
    {

        public IPlacesRepo PlacesRepo { get; set; }
        public IReservationsRepo ReservationsRepo { get; set; }
        public IShowingsRepo ShowingsRepo { get; set; }

        public MockUnitOfWork()
        {
            PlacesRepo = new MockPlacesRepo();
            ReservationsRepo = new MockReservationRepo();
            ShowingsRepo = new MockShowingsRepo();
        }

        public IGenericRepo<T> Repo<T>() where T: class, IGenericModel, new()
        {
            if (typeof(T) == typeof(Place))
                return PlacesRepo as IGenericRepo<T>;
            else if (typeof(T) == typeof(Reservation))
                return ReservationsRepo as IGenericRepo<T>;
            else if (typeof(T) == typeof(Showing))
                return ShowingsRepo as IGenericRepo<T>;
            else
                return new MockGenericRepo<T>() as IGenericRepo<T>;
        }

        public void Dispose()
        {
        }

        public void Save()
        {
        }
    }
}
