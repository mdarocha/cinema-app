using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CinemaApp.Models
{
    public class CinemaEntities : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
    }
}