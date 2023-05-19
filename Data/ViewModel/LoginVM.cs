using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMania.Data.ViewModel
{
    public class LoginVM
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }

        
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is wrong")]

        
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
