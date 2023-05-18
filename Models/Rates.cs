using System.ComponentModel.DataAnnotations.Schema;

namespace MovieMania.Models
{
    public class Rates
    {
        public int Id { get; set; }

        public int MovieId { get; set; }
        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public int Score { get; set; }

        public int Timestamp { get; set; }
    }
}
