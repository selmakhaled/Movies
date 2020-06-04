using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movies.Models
{
    public class Like_Dislike
    {
        [Key]
        public int Id { get; set; }
        public int movieId { get; set; }
        public int userId { get; set; }
        public bool? isLike { set; get; }





    }
}