using System.ComponentModel.DataAnnotations;

namespace MovieMania.Models
{
    public class Genres
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Genre_Movie>? Genremovies { get; set; }
    }
}
