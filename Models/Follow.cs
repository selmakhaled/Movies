using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movies.Models
{
    public class Follow
    {
        [Key]
        public int Id { get; set; }

        public int PublisherId { get; set; }

        public int UserId { get; set; }


    }
}