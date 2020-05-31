using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.ViewModels
{
    public class AdminsMoviesTypesViewModel
    {
        public List<Movie> movies = new List<Movie>();
        public List<Admin> admins = new List<Admin>();
        public List<TypeOfMovie> types = new List<TypeOfMovie>();
        public Movie movie { get; set; }



    }
}