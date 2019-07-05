using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CinemaApp.Models
{
    public class CinemaEntities : DbContext
    {
        public CinemaEntities() : base("CinemaDbConnection")
        {

        }

        public DbSet<Movie> Movies { get; set; }
    }
}