using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Movies.ViewModels
{
    public class userHttpViewModel
    {
        [Display(Name = "username", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "username", ErrorMessageResourceType = typeof(Required))]
        public string Name { get; set; }



        [Required(ErrorMessage = "Password Cant be empty")]
        [DataType(DataType.Password)]
        [Display(Name = "password", ResourceType = typeof(Resource))]
        public string Password { get; set; }



        [Required(ErrorMessage = "Password Confirmation Cant be empty")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        [DataType(DataType.EmailAddress)]
        [Display(Name = "email", ResourceType = typeof(Resource))]

        [Required(ErrorMessageResourceName = "useremail", ErrorMessageResourceType = typeof(Required))]

        public string Email { get; set; }


        [Display(Name = "profilepic", ResourceType = typeof(Resource))]
        public string ImagePath { get; set; }

        [Display(Name = "profilepic", ResourceType = typeof(Resource))]
        [DataType(DataType.Upload)]

        public string file3 { set; get; }









    }
}