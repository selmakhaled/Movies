using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movies.Models
{
    public class Admin

    {
        public int Id { get; set; }

        [Display(Name = "username", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "username", ErrorMessageResourceType = typeof(Required))]


        public string Name { get; set; }


        [Display(Name = "email", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "useremail", ErrorMessageResourceType = typeof(Required))]


        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "userpass", ErrorMessageResourceType = typeof(Required))]


        [Display(Name = "password", ResourceType = typeof(Resource))]
        public string Password { get; set; }


    }
}