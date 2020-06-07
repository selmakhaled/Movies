using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.ViewModels
{
    public class CommentViewModel
    {

        public List<Comments> listOfComments { set; get; }
        public List<USER> listOfUsers { set; get; }

    }
}