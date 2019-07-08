using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CinemaApp.Models;

namespace CinemaApp.DAL
{
    public class TestData : System.Data.Entity.DropCreateDatabaseIfModelChanges<CinemaDbContext>
    {
        protected override void Seed(CinemaDbContext context)
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
                    context.Showings.Add(new Showing
                    {
                        Movie = movie,
                        Time = date,
                    });
                    context.Showings.Add(new Showing
                    {
                        Movie = movie,
                        Time = date.AddHours(2),
                    });
                    date = date.AddDays(1);
                }
            }
            base.Seed(context);
        }
    }
}