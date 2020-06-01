using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Movies.Models;

namespace Movies.ViewModels
{
    public class ActorMovieViewModel
    {


        public Actor actor { set; get; }

        public List<Movie> ActorMovies { set; get; }






    }
}