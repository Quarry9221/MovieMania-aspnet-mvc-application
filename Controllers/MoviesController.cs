using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Data;
using MovieMania.Data;
using MovieMania.Data.Services;
using MovieMania.Data.ViewModel;
using MovieMania.Models;
using System.Data.Common;
using System.Security.Claims;
using PagedList;
using NUnit.Framework;
using Xunit;
using Moq;
using System.Linq.Expressions;

namespace MovieMania.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _service;

        public MoviesController(IMovieService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int page = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var allMovies = await _service.GetAllMoviesAsync();
            var likedMovies = await _service.GetLikedMoviesAsync(userId);

            ViewBag.LikedMovies = likedMovies;

            const int pageSize = 9;
            if (page < 1)
            {
                page = 1;
            }
            int recsCount = allMovies.Count();
            var paging = new Paging(recsCount, page, pageSize);
            int recSkip = (page - 1) * pageSize;
            var data = allMovies.Skip(recSkip).Take(paging.PageSize).ToList();
            this.ViewBag.Paging = paging;


            return View(data);
        }
        public async Task<IActionResult> Short()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var allMovies = await _service.GetAllAsync(n => n.Producer);

            List<Movie> result = new List<Movie>();

            foreach (var movie in allMovies)
            {
                var like = await _service.СheckLikeMovieAsync(movie.Id, userId);

                if (like.IsLiked == true)
                {
                    result.Add(movie);
                }
            }
            return View(result);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _service.GetAllAsync(n => n.Producer);
            int f = 5;

            if (!string.IsNullOrEmpty(searchString))
            {

                var filteredResultNew = allMovies.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResultNew);
            }

            return View("Index", allMovies);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await _service.GetMovieByIdAsync(id);
            return View(movieDetail);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Like(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the user ID
            await _service.LikeMovieAsync(id, userId); // Call the method in the MovieService to add the like
            return RedirectToAction("Index"); // Redirect to the index page after liking the movie
        }


        [Authorize]
        public async Task<IActionResult> Recommendation()
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var allMovies = await _service.GetAllAsync(n => n.Producer);

            //List<Movie> result = new List<Movie>();

            //foreach (var movie in allMovies)
            //{
            //    var like = await _service.СheckLikeMovieAsync(movie.Id, userId);

            //    if (like.IsLiked == true)
            //    {
            //        result.Add(movie);
            //    }
            //}
            //return View(result);


            var loadColumns = new DatabaseLoader.Column[]
                {
                    new DatabaseLoader.Column() {Name = "Id", Type = System.Data.DbType.Int32},
                    new DatabaseLoader.Column() {Name = "UserId", Type = System.Data.DbType.String},
                    new DatabaseLoader.Column() {Name = "MovieId", Type = System.Data.DbType.Int32},
                    new DatabaseLoader.Column() {Name = "Score", Type = System.Data.DbType.Int32},
                    new DatabaseLoader.Column() {Name = "Timestamp", Type = System.Data.DbType.Int32},
                };


            MLContext mlContext = new MLContext();


            var connectionString = @"Data Source=DESKTOP-U8UTJMF;Initial Catalog=mania-movie-app;Integrated Security=True;Connect Timeout=30; TrustServerCertificate=True";
            var connection = new SqlConnection(connectionString);
            var factory = DbProviderFactories.GetFactory(connection);

            var loader = mlContext.Data.CreateDatabaseLoader(loadColumns);



            var dbSource = new DatabaseSource(factory, connectionString, "SELECT * FROM Rates");

            var data = loader.Load(dbSource);

            var preview = data.Preview();


            // var databaseSource = new DatabaseSource(factory, @"Data Source=DESKTOP-U8UTJMF;Initial Catalog=mania-movie-app;Integrated Security=True;Connect Timeout=30", "SELECT * FROM Rates");

            //var trainingView = mlContext.Data.CreateDatabaseLoader<Rates>().Load(databaseSource);
            //DatabaseLoader loader = mLContext.Data.CreateDatabaseLoader<Rates>();


            return View(preview);
        }
    }

}
