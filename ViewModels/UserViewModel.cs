using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movies.ViewModels
{
    public class UserViewModel
    {


        public int Id { set; get; }




        [Display(Name = "username", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "username", ErrorMessageResourceType = typeof(Required))]

        public string Name { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "password", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "userpass", ErrorMessageResourceType = typeof(Required))]

        public string Password { get; set; }



        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        [DataType(DataType.EmailAddress)]
        [Display(Name = "email", ResourceType = typeof(Resource))]

        [Required(ErrorMessageResourceName = "useremail", ErrorMessageResourceType = typeof(Required))]

        public string Email { get; set; }





        [Display(Name = "profilepic", ResourceType = typeof(Resource))]

        public string ImagePath { get; set; }

        [Display(Name = "profilepic", ResourceType = typeof(Resource))]
        [DataType(DataType.Upload)]
        [Required(ErrorMessageResourceName = "userimg", ErrorMessageResourceType = typeof(Required))]

        public string file3 { set; get; }











    }
}