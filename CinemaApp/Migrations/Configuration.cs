namespace CinemaApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using CinemaApp.Models;
    using CinemaApp.DAL;

    internal sealed class Configuration : DbMigrationsConfiguration<CinemaDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "CinemaApp.Models.CinemaDbContext";
        }

        protected override void Seed(CinemaDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            /*for (int i = 0; i < 10; i++)
            {
                var movie = new Movie
                {
                    Title = "Test Movie " + i,
                    Description = "A movie entry made for testing #" + i,
                    PosterUrl = "~/Images/placeholder.png",
                    Runtime = 120
                };
                context.Movies.AddOrUpdate(x => x.ID, movie);

                var date = DateTime.Now;
                for (int j = 0; j < 5; j++)
                {
                    context.Showings.AddOrUpdate(x => x.ID, new Showing
                    {
                        Movie = movie,
                        Time = date,
                        IsSubtitles = true,
                        Is2D = true,
                    });
                    context.Showings.AddOrUpdate(x => x.ID, new Showing
                    {
                        Movie = movie,
                        Time = date.AddHours(2),
                        IsSubtitles = false,
                        Is2D = false,
                    });
                    date = date.AddDays(1);
                }
            } */
        }
    }
}
