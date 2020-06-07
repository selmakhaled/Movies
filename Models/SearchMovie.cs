using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movies.Models
{
    public class SearchMovie
    {




        [DataType(DataType.Text)]
        [Display(Name = "moviename", ResourceType = typeof(Resource))]
        public string Name { get; set; }



        [DataType(DataType.Text)]
        [Display(Name = "moviecountry", ResourceType = typeof(Resource))]
        public string Country { get; set; }

        [Display(Name = "movielanguage", ResourceType = typeof(Resource))]
        [DataType(DataType.Text)]
        public string Language { get; set; }



        [DataType(DataType.DateTime)]
        [Display(Name = "movieproductiondate", ResourceType = typeof(Resource))]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime year_production { get; set; }



        [DataType(DataType.Text)]
        [Display(Name = "movietype", ResourceType = typeof(Resource))]
        public int? type_id { get; set; }




    }





}