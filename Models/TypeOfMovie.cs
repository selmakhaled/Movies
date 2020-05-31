using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace Movies.Models
{
    public class TypeOfMovie
    {
        public int Id { get; set; }
        [Display(Name = "typename", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "type", ErrorMessageResourceType = typeof(Required))]
        public string type_name { get; set; }


    }
}