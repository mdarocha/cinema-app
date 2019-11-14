using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using CinemaApp.Models;

namespace CinemaApp.DAL
{
    public interface ICinemaDbContext
    {
        DbSet<Movie> Movies { get; set; }
        DbSet<Showing> Showings { get; set; }
        DbSet<Reservation> Reservations { get; set; }
        DbSet<Place> Places { get; set; }

        int SaveChanges();
    }

    public class CinemaDbContext : IdentityDbContext<CinemaUser>, ICinemaDbContext
    {
        public CinemaDbContext() : base("CinemaDbConnection")
        {

        }

        public static CinemaDbContext Create()
        {
            return new CinemaDbContext();
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Showing> Showings { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Place> Places { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}