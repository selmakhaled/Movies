using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.ViewModels
{
    public class MoviePublisherViewModel
    {
        public Movie movie { set; get; }
        public string Publisher_Name { set; get; }
    }
}