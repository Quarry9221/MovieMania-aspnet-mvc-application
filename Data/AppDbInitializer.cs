using MovieMania.Data.Enums;
using MovieMania.Models;

namespace MovieMania.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();

                //Actors
                if (!context.Actors.Any())
                {
                    context.Actors.AddRange(new List<Actor>()
                    {
                        new Actor()
                        {
                            Fullname = "Actor 1",
                            Bio = "This is the Bio of the first actor",
                            profilePictureURL = "http://dotnethow.net/images/actors/actor-1.jpeg"
                            
                        },
                        new Actor()
                        {
                            Fullname = "Actor 2",
                            Bio = "This is the Bio of the second actor",
                            profilePictureURL = "http://dotnethow.net/images/actors/actor-2.jpeg"
                        },
                        new Actor()
                        {
                            Fullname = "Actor 3",
                            Bio = "This is the Bio of the second actor",
                            profilePictureURL = "http://dotnethow.net/images/actors/actor-3.jpeg"
                        },
                        new Actor()
                        {
                            Fullname = "Actor 4",
                            Bio = "This is the Bio of the second actor",
                            profilePictureURL = "http://dotnethow.net/images/actors/actor-4.jpeg"
                        },
                        new Actor()
                        {
                            Fullname = "Actor 5",
                            Bio = "This is the Bio of the second actor",
                            profilePictureURL = "http://dotnethow.net/images/actors/actor-5.jpeg"
                        }
                    });
                    context.SaveChanges();
                }
                //Producers
                if (!context.Producers.Any())
                {
                    context.Producers.AddRange(new List<Producer>()
                    {
                        new Producer()
                        {
                            Fullname = "Producer 1",
                            Bio = "This is the Bio of the first actor",
                            profilePictureURL = "http://dotnethow.net/images/producers/producer-1.jpeg"

                        },
                        new Producer()
                        {
                            Fullname = "Producer 2",
                            Bio = "This is the Bio of the second actor",
                            profilePictureURL = "http://dotnethow.net/images/producers/producer-2.jpeg"
                        },
                        new Producer()
                        {
                            Fullname = "Producer 3",
                            Bio = "This is the Bio of the second actor",
                            profilePictureURL = "http://dotnethow.net/images/producers/producer-3.jpeg"
                        },
                        new Producer()
                        {
                            Fullname = "Producer 4",
                            Bio = "This is the Bio of the second actor",
                            profilePictureURL = "http://dotnethow.net/images/producers/producer-4.jpeg"
                        },
                        new Producer()
                        {
                            Fullname = "Producer 5",
                            Bio = "This is the Bio of the second actor",
                            profilePictureURL = "http://dotnethow.net/images/producers/producer-5.jpeg"
                        }
                    });
                    context.SaveChanges();
                }
                //Movies
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>()
                    {
                        new Movie()
                        {
                            Name = "Life",
                            Description = "This is the Life movie description",
                            Price = 39.50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-3.jpeg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(10),
                            ProducerId = 3,
                            MovieCategory = MovieCategory.Documentary
                        },
                        new Movie()
                        {
                            Name = "The Shawshank Redemption",
                            Description = "This is the Shawshank Redemption description",
                            Price = 29.50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-1.jpeg",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(3),
                            ProducerId = 1,
                            MovieCategory = MovieCategory.Action
                        },
                        new Movie()
                        {
                            Name = "Ghost",
                            Description = "This is the Ghost movie description",
                            Price = 39.50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-4.jpeg",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(7),
                            ProducerId = 4,
                            MovieCategory = MovieCategory.Horror
                        },
                        new Movie()
                        {
                            Name = "Race",
                            Description = "This is the Race movie description",
                            Price = 39.50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-6.jpeg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(-5),
                            ProducerId = 2,
                            MovieCategory = MovieCategory.Documentary
                        },
                        new Movie()
                        {
                            Name = "Scoob",
                            Description = "This is the Scoob movie description",
                            Price = 39.50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-7.jpeg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(-2),
                            ProducerId = 3,
                            MovieCategory = MovieCategory.Cartoon
                        },
                        new Movie()
                        {
                            Name = "Cold Soles",
                            Description = "This is the Cold Soles movie description",
                            Price = 39.50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-8.jpeg",
                            StartDate = DateTime.Now.AddDays(3),
                            EndDate = DateTime.Now.AddDays(20),
                            ProducerId = 5,
                            MovieCategory = MovieCategory.Drama
                        }
                    });
                    context.SaveChanges();
                }
                //Genres
                if (!context.Genres.Any())
                {
                    context.Genres.AddRange(new List<Genres>()
                    {
                        new Genres()
                        {
                            Name = "Action"
                        },
                        new Genres()
                        {
                            Name = "Cartoon"
                        },
                        new Genres()
                        {
                            Name = "Drama"
                        },
                        new Genres()
                        {
                            Name = "Horror"
                        },
                        new Genres()
                        {
                            Name = "Documentary"
                        },
                        new Genres()
                        {
                            Name = "Comedy"
                        },
                        new Genres()
                        {
                            Name = "Romance"
                        },
                        new Genres()
                        {
                            Name = "Thriller"
                        },
                        new Genres()
                        {
                            Name = "Sci-Fi"
                        },
                        new Genres()
                        {
                            Name = "Adventure"
                        },
                        new Genres()
                        {
                            Name = "Crime"
                        },
                        new Genres()
                        {
                            Name = "Mystery"
                        },
                        new Genres()
                        {
                            Name = "Fantasy"
                        },
                        new Genres()
                        {
                            Name = "Family"
                        },
                        new Genres()
                        {
                            Name = "War"
                        }
                    });
                    context.SaveChanges();
                }
                //Actors & Movies
                if (!context.Actors_Movies.Any())
                {
                    context.Actors_Movies.AddRange(new List<Actor_Movie>()
                    {
                        new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 19
                        },
                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 19
                        },

                         new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 20
                        },
                         new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 20
                        },

                        new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 21
                        },
                        new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 21
                        },
                        new Actor_Movie()
                        {
                            ActorId = 5,
                            MovieId = 21
                        },


                        new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 22
                        },
                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 22
                        },
                        new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 22
                        },


                        new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 23
                        },
                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 23
                        },
                        new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 23
                        },
                        new Actor_Movie()
                        {
                            ActorId = 5,
                            MovieId = 23
                        },


                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 24
                        },
                        new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 24
                        },
                        new Actor_Movie()
                        {
                            ActorId = 5,
                            MovieId = 24
                        },
                    });
                    context.SaveChanges();
                }
                if (!context.GenreMovies.Any())
                {
                    context.GenreMovies.AddRange(new List<Genre_Movie>()
                    {
                         new Genre_Movie()
                        {
                            GenreId = 1,
                            MovieId = 20
                        },
                         new Genre_Movie()
                        {
                            GenreId = 4,
                            MovieId = 20
                        },
                         new Genre_Movie()
                         {
                             GenreId = 5,
                            MovieId = 21
                         },
                         new Genre_Movie()
                         {
                             GenreId = 5,
                            MovieId = 22
                         },
                         new Genre_Movie()
                         {
                             GenreId = 6,
                            MovieId = 22
                         },
                         new Genre_Movie()
                         {
                             GenreId = 6,
                            MovieId = 23
                         },
                         new Genre_Movie()
                         {
                             GenreId = 6,
                            MovieId = 24
                         },
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
