using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Movies.Models
{
    public class Movie

    {
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "moviename", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "moviename", ErrorMessageResourceType = typeof(Required))]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "movieduration", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "movieduration", ErrorMessageResourceType = typeof(Required))]
        public string Duration { get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "moviecountry", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "moviecountry", ErrorMessageResourceType = typeof(Required))]
        public string Country { get; set; }

        [Display(Name = "movielanguage", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "movielanguage", ErrorMessageResourceType = typeof(Required))]
        [DataType(DataType.Text)]
        public string Language { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "movieuploaddate", ResourceType = typeof(Resource))]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Upload_Date { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "movieproductiondate", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "movieprodate", ErrorMessageResourceType = typeof(Required))]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime year_production { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessageResourceName = "desc", ErrorMessageResourceType = typeof(Required))]
        [Display(Name = "moviedescription", ResourceType = typeof(Resource))]
        public string description { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "movietype", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "movietype", ErrorMessageResourceType = typeof(Required))]
        public int type_id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "movievideo", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "movievideo", ErrorMessageResourceType = typeof(Required))]
        public string MovieDirectroy { get; set; }



        public string Trailer { set; get; }

        [DataType(DataType.Text)]
        [Display(Name = "publishername", ResourceType = typeof(Resource))]



        public string publisher_Name { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "movietype", ResourceType = typeof(Resource))]
        public string Type_Name { get; set; }
        [DataType(DataType.Text)]
        public int pulisher_id { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "movieposter", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "movieposter", ErrorMessageResourceType = typeof(Required))]
        public string ImagePath { get; set; }
        [Display(Name = "Movie Cover ")]
        public string CoverPath { set; get; }


        public string Cast { set; get; }

        public int Seen { get; set; }

    }
}