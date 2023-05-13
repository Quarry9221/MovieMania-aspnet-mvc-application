using MovieMania.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace MovieMania.Models
{
    public class Actor : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage ="REQUIRED")]
        public string profilePictureURL { get; set; }
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "REQUIRED")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The name must be between 3 and 50 characters long.")]
        public string Fullname { get; set; }
        [Display(Name = "Biography")]
        [Required(ErrorMessage = "REQUIRED")]
        public string Bio { get; set; }

        public List<Actor_Movie>? Actors_Movies { get; set; }
    }
}
