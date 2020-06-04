using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.ViewModels
{
    public class UserProfileViewModel
    {
        public USER user { get; set; }
        public List<Movie> Listfavouritesmovies { get; set; }

        public List<Follow> followinglist { set; get; }

        public List<Movie> follwingPublisherMovies { get; set; }

        public List<Admin> publishersList { set; get; }

        public List<Movie> likeList { set; get; }



    }
}