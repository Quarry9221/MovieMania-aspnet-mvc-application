using MovieMania.Data.Base;
using MovieMania.Data.ViewModel;
using MovieMania.Models;

namespace MovieMania.Data.Services
{
    public interface IMovieService : IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues();
        Task AddNewMovieAsync(NewMovieVM data);
        Task UpdateMovieAsync(NewMovieVM data);
        Task<Movie> LikeMovieAsync(int movieId, string userId);

        Task<Movie> СheckLikeMovieAsync(int movieId, string userId);

        Task<IEnumerable<Movie>> GetLikedMoviesAsync(string userId);
    }
}
