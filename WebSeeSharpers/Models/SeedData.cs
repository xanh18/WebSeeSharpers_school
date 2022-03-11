using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebSeeSharpers.Data;
using System;
using System.Linq;
using WebSeeSharpers.Models;

namespace WebSeeSharpers.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new WebSeeSharpersContext(
                   serviceProvider.GetRequiredService<
                       DbContextOptions<WebSeeSharpersContext>>()))
        {
            // Look for any movies.
            if (context.Movie.Any())
            {
                return;   // DB has been seeded
            }

            context.Movie.AddRange(
                new Movie
                {
                    Title = "When Harry Met Sally",
                    Duration = new TimeSpan(2, 10, 0),
                    Genre = "Romantic Comedy",
                    Movie3d = false,
                    BeginTime = new DateTime(2022, 5, 15, 13, 45, 0),
                    AgeRequirement = 12,
                    Thumbnail = "https://media.s-bol.com/822q0MkjXEGo/847x1200.jpg",
                    Description = "Harry and Sally have known each other for years, and are very good friends, but they fear sex would ruin the friendship."

                },

                new Movie
                {
                    Title = "Harry potter and the deadly hallows",
                    Duration = new TimeSpan(2, 0, 0),
                    Genre = "Fantasy",
                    Movie3d = false,
                    BeginTime = new DateTime(2022, 5, 15, 13, 45, 0),
                    AgeRequirement = 12,
                    Thumbnail = "https://media.s-bol.com/qPrGlVlPgJr/550x730.jpg",
                    Description = "As Harry, Ron, and Hermione race against time and evil to destroy the Horcruxes, they uncover the existence of the three most powerful objects in the wizarding world: the Deathly Hallows."

                }

            );
            context.SaveChanges();
        }
    }
}