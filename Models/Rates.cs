using Microsoft.ML.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieMania.Models
{
    public class Rates
    {
        public int Id { get; set; }

        [ColumnName("UserId"), LoadColumn(0)]
        public string UserId { get; set; }
        [ForeignKey("UserId")]

        [ColumnName("MovieId"), LoadColumn(1)]
        public int MovieId { get; set; }
        
        [ForeignKey("MovieId")]
        
        [ColumnName("Label"), LoadColumn(2)]
        public float Label { get; set; }

    }

    public class MovieRatingPrediction
    {
        public float Label;

        public float Score;
    }
}
