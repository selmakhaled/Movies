using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movies.ViewModels
{
    public class Login
    {

        [DataType(DataType.EmailAddress)]
        [Display(Name = "email", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "useremail", ErrorMessageResourceType = typeof(Required))]

        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "password", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "userpass", ErrorMessageResourceType = typeof(Required))]

        public string Password { get; set; }


    }
}