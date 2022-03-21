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
                    Genre = "Romantische Komedie",
                    GenreEn = "Romantic Comedy",
                    Movie3d = false,
                    BeginTime = new DateTime(2022, 5, 15, 13, 30, 0),
                    AgeRequirement = 12,
                    Thumbnail = "https://media.s-bol.com/822q0MkjXEGo/847x1200.jpg",
                    Description = "Harry en Sally kennen elkaar al jaren en zijn erg goede vrienden, maar ze zijn bang dat seks de vriendschap zou verpesten.",
                    DescriptionEn = "Harry and Sally have known each other for years, and are very good friends, but they fear sex would ruin the friendship."

                });

      /*          new Movie
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
                },

                new Movie
                {
                    Title = "SPIDER-MAN: NO WAY HOME",
                    Duration = new TimeSpan(2, 15, 0),
                    Genre = "Action, Fantasy",
                    Movie3d = false,
                    BeginTime = new DateTime(2021, 5, 15, 13, 32, 0),
                    AgeRequirement = 16,
                    Thumbnail = "https://media.pathe.nl/nocropthumb/620x955/gfx_content/Spider-Man_-No-Way-Home_ps_1_jpg_sd-high_Copyright-MARVEL-2021-CPII-All-Rights-Reserved.jpg",
                    Description = "For the first time in the cinematic history of Spider-Man, our friendly neighborhood hero's identity is revealed, bringing his Super Hero responsibilities into conflict with his normal life and putting those he cares about most at risk. When he enlists Doctor Strange's help to restore his secret, the spell tears a hole in their world, releasing the most powerful villains who've ever fought a Spider-Man in any universe. Now, Peter will have to overcome his greatest challenge yet, which will not only forever alter his own future but the future of the Multiverse.",
                },


                new Movie
                {
                    Title = "Inside-Out",
                    Duration = new TimeSpan(1, 45, 0),
                    Genre = "Kids",
                    Movie3d = false,
                    BeginTime = new DateTime(2021, 5, 15, 13, 32, 0),
                    AgeRequirement = 0,
                    Thumbnail = "https://upload.wikimedia.org/wikipedia/en/0/0a/Inside_Out_%282015_film%29_poster.jpg",
                    Description = "Riley (Kaitlyn Dias) is a happy, hockey-loving 11-year-old Midwestern girl, but her world turns upside-down when she and her parents move to San Francisco. Riley's emotions -- led by Joy (Amy Poehler) -- try to guide her through this difficult, life-changing event. However, the stress of the move brings Sadness (Phyllis Smith) to the forefront. When Joy and Sadness are inadvertently swept into the far reaches of Riley's mind, the only emotions left in Headquarters are Anger, Fear and Disgust.",
                },

                new Movie
                {
                    Title = "Finding Dory",
                    Duration = new TimeSpan(1, 55, 0),
                    Genre = "Kids",
                    Movie3d = true,
                    BeginTime = new DateTime(2021, 5, 15, 13, 32, 0),
                    AgeRequirement = 0,
                    Thumbnail = "https://flxt.tmsimg.com/assets/p12329215_p_v8_ap.jpg",
                    Description = "Dory (Ellen DeGeneres) is a wide-eyed, blue tang fish who suffers from memory loss every 10 seconds or so. The one thing she can remember is that she somehow became separated from her parents as a child. With help from her friends Nemo and Marlin, Dory embarks on an epic adventure to find them. Her journey brings her to the Marine Life Institute, a conservatory that houses diverse ocean species. Dory now knows that her family reunion will only happen if she can save mom and dad from captivity.",
                });*/

            // Look for any Theatres.
            if (context.Theatres.Any())
            {
                return;   // DB has been seeded
            }
            

            context.Theatres.AddRange(

               

                new Theatre
                {
                    Number = 1,
                    AmountOfRows = 15,
                    AmountOfSeats = 150,

                },

                new Theatre
                {
                    Number = 2,
                    AmountOfRows = 20,
                    AmountOfSeats = 200,

                },

                new Theatre
                {
                    Number = 3,
                    AmountOfRows = 15,
                    AmountOfSeats = 150,

                },

                new Theatre
                {
                    Number = 4,
                    AmountOfRows = 20,
                    AmountOfSeats = 200,

                },

                new Theatre
                {
                    Number = 5,
                    AmountOfRows = 10,
                    AmountOfSeats = 100,

                }
                );


            // Look for any Theatres.
            if (context.Viewings.Any())
            {
                return;   // DB has been seeded
            }


            context.Viewings.AddRange(

   /*             new Viewing
                {
                    StartDateTime = new DateTime(2022, 5, 15, 13, 30, 0),
                    Movie = new Movie
                    {
                        Title = "Inside-Out",
                        Duration = new TimeSpan(1, 45, 0),
                        Genre = "Kids",
                        Movie3d = false,
                        BeginTime = new DateTime(2021, 5, 15, 13, 32, 0),
                        AgeRequirement = 0,
                        Thumbnail = "https://upload.wikimedia.org/wikipedia/en/0/0a/Inside_Out_%282015_film%29_poster.jpg",
                        Description = "Riley (Kaitlyn Dias) is a happy, hockey-loving 11-year-old Midwestern girl, but her world turns upside-down when she and her parents move to San Francisco. Riley's emotions -- led by Joy (Amy Poehler) -- try to guide her through this difficult, life-changing event. However, the stress of the move brings Sadness (Phyllis Smith) to the forefront. When Joy and Sadness are inadvertently swept into the far reaches of Riley's mind, the only emotions left in Headquarters are Anger, Fear and Disgust.",
                    },
                    Theatre = new Theatre
                    {
                        Number = 5,
                        AmountOfRows = 10,
                        AmountOfSeats = 100,

                    }

                },*/

                new Viewing
                {

                    StartDateTime = new DateTime(2022, 5, 15, 14, 30, 0),
                    Movie = new Movie
                    {
                        Title = "James Bond, Spectre",
                        Duration = new TimeSpan(2, 10, 0),
                        Genre = "Action, Thriller",
                        GenreEn = "Actie, Thriller",
                        Movie3d = false,
                        BeginTime = new DateTime(2022, 5, 17, 13, 30, 0),
                        AgeRequirement = 16,
                        Thumbnail = "https://www.vprogids.nl/.imaging/mte/gids/textimage-left/dam/cinema/13/79/68/image_13796859.jpeg/jcr:content/image_13796859.jpeg",
                        DescriptionEn = "A cryptic message from James Bond's past sends him on a trail to uncover the existence of a sinister organisation named SPECTRE. With a new threat dawning, Bond learns the terrible truth about the author of all his pain in his most recent missions.",
                        Description = "Een cryptische boodschap uit het verleden van James Bond stuurt hem op een spoor om het bestaan van een sinistere organisatie genaamd SPECTRE te ontdekken. Met een nieuwe dreiging die aanbreekt, ontdekt Bond de verschrikkelijke waarheid over de auteur van al zijn pijn tijdens zijn meest recente missies."
                    },
                    Theatre = new Theatre
                    {
                        Number = 1,
                        AmountOfRows = 15,
                        AmountOfSeats = 150,

                    }
                }
                );



            context.SaveChanges();


        }

    }
}