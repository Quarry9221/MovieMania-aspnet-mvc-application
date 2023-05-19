using System.ComponentModel.DataAnnotations;

namespace MovieMania.Models
{
    public class Producer
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture")]

        public int TMDBId { get; set; }
        public string profilePictureURL { get; set; }
        [Display(Name = "Full Name")]
        public string Fullname { get; set; }
        [Display(Name = "Biography")]
        public string Bio { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
