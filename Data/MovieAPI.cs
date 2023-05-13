using Newtonsoft.Json;
using TMDbLib;
using TMDbLib.Client;

namespace MovieMania.Data
{
    public class MovieAPI
    {
        TMDbClient client = new TMDbClient("456ea984312e623550e3d506d8d72e7f");
        string API = "456ea984312e623550e3d506d8d72e7f";
        string Base_URL = "https://api.themoviedb.org/3/";
        string API_URL = "https://api.themoviedb.org/3/movie/550?api_key=456ea984312e623550e3d506d8d72e7f";
        public void getmovie()
        {
            var client = new TMDbClient(API);
            var movie = client.GetMovieAsync(550).Result;
            var json = JsonConvert.SerializeObject(movie);
            Console.WriteLine(json);
        }
        
    }
    
}
