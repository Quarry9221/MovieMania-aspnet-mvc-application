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
using Microsoft.ML.Trainers;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

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
            var allMovies = await _service.GetAllMoviesAsync();

            if (!string.IsNullOrEmpty(searchString))
            {

                var filteredResultNew = allMovies.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResultNew);
            }

            return View("Index", allMovies);
        }
        [HttpPost]
        public IActionResult Rate(int id, int rating)
        {
            // Retrieve the movie with the given id from the database
            var movie =  _service.GetMovieByIdAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            // Save the rating to the database or perform any other necessary logic
            // ...

            // Redirect back to the movie list page
            return RedirectToAction("Index");
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
            string userId = User.Identity.GetUserId();

            var loadColumns = new DatabaseLoader.Column[]
                {
                    new DatabaseLoader.Column() {Name = "UserId", Type = System.Data.DbType.String},
                    new DatabaseLoader.Column() {Name = "MovieId", Type = System.Data.DbType.Int32},
                    new DatabaseLoader.Column() {Name = "Label", Type = System.Data.DbType.Single},
                };

            var connectionString = @"Data Source=DESKTOP-U8UTJMF;Initial Catalog=mania-movie-app;Integrated Security=True;Connect Timeout=30; TrustServerCertificate=True";
            var connection = new SqlConnection(connectionString);
            var factory = DbProviderFactories.GetFactory(connection);

            MLContext mlContext = new MLContext();
            var loader = mlContext.Data.CreateDatabaseLoader(loadColumns);



            var dbSource = new DatabaseSource(factory, connectionString, "SELECT * FROM Rates");

            IDataView data = loader.Load(dbSource);

            //var data = loader.Load(dbSource);

            //var preview = data.Preview();

            var testTrainSplit = mlContext.Data.TrainTestSplit(data, testFraction: 0.2);

            IDataView Trainset = testTrainSplit.TrainSet;
            IDataView Testset = testTrainSplit.TestSet;

            ITransformer model = BuildAndTrainModel(mlContext,Trainset);

            EvaluateModel(mlContext, Testset, model);

            Rates Rated = UseModelForSinglePrediction(mlContext, model);

            ITransformer BuildAndTrainModel(MLContext mlContext, IDataView trainingDataView)
            {
                IEstimator<ITransformer> estimator = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "UserIdEncoded", inputColumnName: "UserId")
                .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "MovieIdEncoded", inputColumnName: "MovieId"));

                var options = new MatrixFactorizationTrainer.Options
                {
                    MatrixColumnIndexColumnName = "UserIdEncoded",
                    MatrixRowIndexColumnName = "MovieIdEncoded",
                    LabelColumnName = "Label",
                    NumberOfIterations = 20,
                    ApproximationRank = 100
                };

                var trainerEstimator = estimator.Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));
                Console.WriteLine("=============== Training the model ===============");
                ITransformer model = trainerEstimator.Fit(trainingDataView);

                return model;
            }

            void EvaluateModel(MLContext mlContext, IDataView testDataView, ITransformer model)
            {
                Console.WriteLine("=============== Evaluating the model ===============");
                var prediction = model.Transform(testDataView);
                var metrics = mlContext.Regression.Evaluate(prediction, labelColumnName: "Label", scoreColumnName: "Score");
                Console.WriteLine("Root Mean Squared Error : " + metrics.RootMeanSquaredError.ToString());
                Console.WriteLine("RSquared: " + metrics.RSquared.ToString());
            }

            Rates UseModelForSinglePrediction(MLContext mlContext, ITransformer model)
            {
                Random rand = new Random();
                int rndnumb = rand.Next(10, 1600);
                Console.WriteLine("=============== Making a prediction ===============");
                var predictionEngine = mlContext.Model.CreatePredictionEngine<Rates, MovieRatingPrediction>(model);

                var testInput = new Rates { UserId = userId, MovieId = rndnumb };

                var movieRatingPrediction = predictionEngine.Predict(testInput);
                Console.WriteLine(movieRatingPrediction.Score);
                if (Math.Round(movieRatingPrediction.Score, 1) > 3.5)
                {
                    ViewBag.MyMessage = "This movie is recommended for you";
                }
                else
                {
                    ViewBag.MyMessage = "This movie is not recommended for you";
                }
                return testInput;
            }
            var movieDetail = await _service.GetMovieByIdAsync(Rated.MovieId);

            var features = data.Schema.Select(col => col.Name).Where(colName => colName != "Score").ToArray();

            //var pipeline = mlContext.Transforms.Text.FeaturizeText("Text",)
            // var databaseSource = new DatabaseSource(factory, @"Data Source=DESKTOP-U8UTJMF;Initial Catalog=mania-movie-app;Integrated Security=True;Connect Timeout=30", "SELECT * FROM Rates");

            //var trainingView = mlContext.Data.CreateDatabaseLoader<Rates>().Load(databaseSource);
            //DatabaseLoader loader = mLContext.Data.CreateDatabaseLoader<Rates>();


            return View(movieDetail);
        }
    }

}
