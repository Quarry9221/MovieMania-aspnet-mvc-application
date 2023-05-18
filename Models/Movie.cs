using MovieMania.Data.Base;
using MovieMania.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TMDbLib.Objects.General;

namespace MovieMania.Models
{
    public class Movie : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string production_companies { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public DateTime StartDate { get; set; }
        public MovieCategory MovieCategory { get; set; }
        public bool IsLiked { get; set; }
        public List<Actor_Movie> ActorMovies { get; set; }
        public Like Likes { get; set; }
        public List<Genre_Movie>? GenreMovies { get; set; }
        public int ProducerId { get; set; }
        [ForeignKey("ProducerId")]
        public Producer Producer { get; set; }

        public float averageRate { get; set; }

        public float popularity { get; set; }
    }

    public class MovieRatingPrediction
    {
        public float Label;
        
        public float Score;
    }
}
