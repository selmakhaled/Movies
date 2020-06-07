using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.ViewModels
{
    public class FavoriteLaterMovie
    {

        public Movie movie { set; get; }
        public bool Favo { set; get; }
        //public bool exinwatchlater { set; get; }
        public Like_Dislike likes_dislike { get; set; }
        public List<Like_Dislike> likesdislist { get; set; }
        public Comments Comment { get; set; }
        public List<Comments> listOfComments { set; get; }
        public List<Movie> listOfMovies { set; get; }
        public List<USER> listOfUsers { set; get; }
        public int followFlag { get; set; }
        public CommentViewModel x = new CommentViewModel();
        public List<Actor> listOfActors { set; get; }


    }
}