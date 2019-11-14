using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Data.Entity;
using CinemaApp.Models;

namespace CinemaApp.DAL
{
    public interface IGenericRepo<T>
    {
        void Add(T e);
        void Update(T e);
        void Remove(T e);

        T Get(int id);
        T Get(Expression<Func<T, bool>> p);

        IEnumerable<T> GetList();
        IEnumerable<T> GetList(Expression<Func<T, bool>> p);
    }

    public class GenericRepo<T> : IGenericRepo<T> where T : class, IGenericModel
    {
        private CinemaDbContext context;
        protected DbSet<T> set;

        public GenericRepo(CinemaDbContext context)
        {
            this.context = context;
            set = context.Set<T>();
        }

        public void Add(T entity)
        {
            set.Add(entity);
        }

        public void Update(T entity)
        {
            set.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            if(context.Entry(entity).State == EntityState.Detached)
            {
                set.Attach(entity);
            }

            set.Remove(entity);
        }

        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            return set.First(predicate);
        }

        public virtual T Get(int id)
        {
            return set.Single(e => e.ID == id);
        }

        public virtual IEnumerable<T> GetList(Expression<Func<T, bool>> predicate = null)
        {
            return set.Where(predicate);
        }

        public virtual IEnumerable<T> GetList()
        {
            return set.AsEnumerable();
        }
    }
}