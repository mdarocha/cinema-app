using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CinemaApp.Models;

namespace CinemaApp.DAL
{
    public class TestData : System.Data.Entity.DropCreateDatabaseIfModelChanges<CinemaEntities>
    {
        protected override void Seed(CinemaEntities context)
        {
            for (int i = 0; i < 10; i++)
            {
                context.Movies.Add(new Movie
                {
                    Title = "Test Movie " + i,
                    Description = "A movie entry made for testing #" + i,
                    PosterUrl = "/Images/placeholder.png",
                    Runtime = 120
                });
            }
            base.Seed(context);
        }
    }
}