using Microsoft.EntityFrameworkCore;
using MovieMania.Data.Base;
using MovieMania.Data.ViewModel;
using MovieMania.Models;

namespace MovieMania.Data.Services
{
    public class MovieService : EntityBaseRepository<Movie>, IMovieService
    {
        private readonly AppDbContext _context;
        public MovieService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewMovieAsync(NewMovieVM data)
        {
            var newMovie = new Movie()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                ProducerId = data.ProducerId
            };
            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await _context.Movies
                .Include(p => p.Producer)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor).Include(gm => gm.Genremovies).ThenInclude(g => g.Genre)
                .FirstOrDefaultAsync(n => n.Id == id);

            return movieDetails;
        }

        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues()
        {
            var response = new NewMovieDropdownsVM()
            {
                Actors = await _context.Actors.OrderBy(n => n.Fullname).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.Fullname).ToListAsync()
            };

            return response;
        }

        public async Task UpdateMovieAsync(NewMovieVM data)
        {
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbMovie != null)
            {
                dbMovie.Name = data.Name;
                dbMovie.Description = data.Description;
                dbMovie.Price = data.Price;
                dbMovie.ImageURL = data.ImageURL;
                dbMovie.StartDate = data.StartDate;
                dbMovie.EndDate = data.EndDate;
                dbMovie.MovieCategory = data.MovieCategory;
                dbMovie.ProducerId = data.ProducerId;
                await _context.SaveChangesAsync();
            }

            //Remove existing actors
            var existingActorsDb = _context.Actors_Movies.Where(n => n.MovieId == data.Id).ToList();
            _context.Actors_Movies.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = data.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }
        public  async Task<Movie> LikeMovieAsync(int movieId, string userId)
        {
            var movie = await _context.Movies.FindAsync(movieId);

            var like = await _context.Likes
                .FirstOrDefaultAsync(l => l.MovieId == movieId && l.UserId == userId);

            if (like == null)
            {
                // User has not liked the movie, so create a new like record
                like = new Like()
                {
                    MovieId = movieId,
                    UserId = userId,
                    IsLiked = true
                };
                movie.IsLiked = true;
                await _context.Likes.AddAsync(like);
                await _context.SaveChangesAsync();


            }
            else
            {
                // User has already liked the movie, so remove the like record
                movie.IsLiked = false;
                _context.Likes.Remove(like);
                await _context.SaveChangesAsync();


            }

            return movie;

        }
        public async Task<Movie> СheckLikeMovieAsync(int movieId, string userId)
        {
            var movie = await _context.Movies.FindAsync(movieId);

            var like = await _context.Likes
                .FirstOrDefaultAsync(l => l.MovieId == movieId && l.UserId == userId);

            return movie;
        }

        public async Task<IEnumerable<Movie>> GetLikedMoviesAsync(string userId)
        {
            // Retrieve the liked movies for the specified user
            var likedMovies = await _context.Likes
                .Where(l => l.UserId == userId && l.IsLiked)
                .Select(l => l.Movie)
                .ToListAsync();

            return likedMovies;
        }




     }
}
