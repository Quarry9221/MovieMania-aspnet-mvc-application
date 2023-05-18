using MovieMania.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMania.Data.ViewModel
{
    public class NewMovieDropdownsVM
    {
        public NewMovieDropdownsVM()
        {
            Producers = new List<Producer>();
            Actors = new List<Actor>();
        }

        public List<Producer> Producers { get; set; }
        public List<Actor> Actors { get; set; }
    }
}
