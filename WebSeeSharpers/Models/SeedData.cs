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
                    BeginTime = new DateTime(2022, 5, 15, 13, 30, 0),
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
                    AgeRequirement = 12,
                    Thumbnail = "https://media.s-bol.com/qPrGlVlPgJr/550x730.jpg",
                    Description = "As Harry, Ron, and Hermione race against time and evil to destroy the Horcruxes, they uncover the existence of the three most powerful objects in the wizarding world: the Deathly Hallows."

                },

                new Movie
                {
                    Title = "The Hobbit",
                    Duration = new TimeSpan(1, 50, 0),
                    Genre = "Fantasy",
                    Movie3d = false,
                    BeginTime = new DateTime(2022, 5, 16, 13, 30, 0),
                    AgeRequirement = 12,
                    Thumbnail = "https://media.pathe.nl/nocropthumb/620x955/gfx_content/posters/hobbitbattlefivearmiesposter6b.jpg",
                    Description = "A reluctant Hobbit, Bilbo Baggins, sets out to the Lonely Mountain with a spirited group of dwarves to reclaim their mountain home, and the gold within it from the dragon Smaug."
                },

                new Movie
                {
                    Title = "James Bond, Spectre",
                    Duration = new TimeSpan(2, 10, 0),
                    Genre = "Action, Thriller",
                    Movie3d = false,
                    BeginTime = new DateTime(2022, 5, 17, 13, 30, 0),
                    AgeRequirement = 16,
                    Thumbnail = "https://www.vprogids.nl/.imaging/mte/gids/textimage-left/dam/cinema/13/79/68/image_13796859.jpeg/jcr:content/image_13796859.jpeg",
                    Description = "A cryptic message from James Bond's past sends him on a trail to uncover the existence of a sinister organisation named SPECTRE. With a new threat dawning, Bond learns the terrible truth about the author of all his pain in his most recent missions."
                }



            );
         



            // Look for any Theatres.
            if (context.Theatres.Any())
            {
                return;   // DB has been seeded
            }
            

            context.Theatres.AddRange(

                new Theatre
                {
                    Number = 2,
                    AmountOfRows = 20,
                    AmountOfSeats = 200,

                },

                new Theatre
                {
                    Number = 1,
                    AmountOfRows = 15,
                    AmountOfSeats = 150,

                }
                );


            // Look for any Theatres.
            if (context.Viewings.Any())
            {
                return;   // DB has been seeded
            }


            context.Viewings.AddRange(

                new Viewing
                {
                    StartDateTime = new DateTime(2022, 5, 15, 13, 30, 0),

                },

                new Viewing
                {

                    StartDateTime = new DateTime(2022, 5, 15, 14, 30, 0),
                }
                );



            context.SaveChanges();


        }

    }
}