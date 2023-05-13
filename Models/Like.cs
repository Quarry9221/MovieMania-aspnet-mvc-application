namespace MovieMania.Models
{
    public class Like
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public List<Movie> likedMovies { get; set; }
    }
}
