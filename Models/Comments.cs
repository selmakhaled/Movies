using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movies.Models
{
    public class Comments
    {
        [Key]

        public int commentId { get; set; }

        public int userId { get; set; }


        public int movieId { get; set; }

        [Required(ErrorMessage = "Comment can't be empty !!!")]
        public string comment { get; set; }

        public DateTime commentDate { get; set; }




    }
}