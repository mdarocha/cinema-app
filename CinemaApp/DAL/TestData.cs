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
                var movie = new Movie
                {
                    Title = "Test Movie " + i,
                    Description = "A movie entry made for testing #" + i,
                    PosterUrl = "~/Images/placeholder.png",
                    Runtime = 120
                };
                context.Movies.Add(movie);

                var date = DateTime.Now;
                for (int j = 0; j < 5; j++)
                {
                    var showing = new Showing
                    {
                        Movie = movie,
                        Time = date,
                    };
                    context.Showings.Add(showing);
                    date = date.AddDays(1);
                }
            }
            base.Seed(context);
        }
    }
}