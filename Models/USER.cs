using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movies.Models
{
    public class USER

    {
        public int Id { get; set; }
        [Display(Name = "username", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "username", ErrorMessageResourceType = typeof(Required))]

        public string Name { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "password", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "userpass", ErrorMessageResourceType = typeof(Required))]

        public string Password { get; set; }



        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The Password and Confirmation password do not match.")]
        public string ConfirmPassword { get; set; }







        [DataType(DataType.EmailAddress)]
        [Display(Name = "email", ResourceType = typeof(Resource))]

        [Required(ErrorMessageResourceName = "useremail", ErrorMessageResourceType = typeof(Required))]

        public string Email { get; set; }




        [Required(ErrorMessageResourceName = "userimg", ErrorMessageResourceType = typeof(Required))]

        [Display(Name = "profilepic", ResourceType = typeof(Resource))]

        public string ImagePath { get; set; }

        [Display(Name = "profilepic", ResourceType = typeof(Resource))]
        [DataType(DataType.Upload)]
        [NotMapped]
        public string file3 { set; get; }


        [NotMapped]
        public string currentPassword { get; set; }


    }



}