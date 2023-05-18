using TMDbLib.Objects.General;

namespace MovieMania.Models
{
    public class Genre_Movie
    {
        public int GenreId { get; set; }
        public Genres Genre { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
