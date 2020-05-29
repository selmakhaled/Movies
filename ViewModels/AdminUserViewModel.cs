using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.ViewModels
{
    public class AdminUserViewModel
    {
        public Admin admin { set; get; }

        public USER user { set; get; }
    }
}