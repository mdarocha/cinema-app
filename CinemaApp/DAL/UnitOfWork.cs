using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CinemaApp.Models;

namespace CinemaApp.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepo<T> Repo<T>() where T: class, IGenericModel, new();
        void Save();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private CinemaDbContext db;
        private Dictionary<Type, object> repo;

        public UnitOfWork()
        {
            db = new CinemaDbContext();
            repo = new Dictionary<Type, object>();
        }

        public IGenericRepo<T> Repo<T>() where T: class, IGenericModel, new()
        {
            if (repo.Keys.Contains(typeof(T)))
            {
                return repo[typeof(T)] as IGenericRepo<T>;
            }
            else
            {
                IGenericRepo<T> r;
                if(typeof(T) == typeof(Place))
                {
                    r = new PlacesRepo(db) as IGenericRepo<T>;
                } else if(typeof(T) == typeof(Reservation))
                {
                    r = new ReservationsRepo(db) as IGenericRepo<T>;
                } else if(typeof(T) == typeof(Showing))
                {
                    r = new ShowingsRepo(db) as IGenericRepo<T>;
                }
                else
                {
                    r = new GenericRepo<T>(db) as IGenericRepo<T>;
                }

                repo.Add(typeof(T), r);
                return r;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}