using Movies.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Movies.ViewModels
{
    public class MovieTypesViewModel
    {


        public List<TypeOfMovie> TypesList = new List<TypeOfMovie>();

        // image of movie
        [DataType(DataType.Upload)]
        [Display(Name = "movieposter", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "file", ErrorMessageResourceType = typeof(Required))]
        public string file { get; set; }

        //video of movie
        [DataType(DataType.Upload)]
        [Display(Name = "movievideo", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "file2", ErrorMessageResourceType = typeof(Required))]
        public string file2 { get; set; }



        //cover of movie
        [DataType(DataType.Upload)]
        [Display(Name = "Movie Cover")]
        public string file3 { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Movie Trailer")]

        public string file4 { get; set; }


        public int Id { get; set; }

        [Display(Name = "moviename", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "moviename", ErrorMessageResourceType = typeof(Required))]
        public string Name { get; set; }

        [Display(Name = "movieduration", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "movieduration", ErrorMessageResourceType = typeof(Required))]
        public string Duration { get; set; }

        [Display(Name = "moviecountry", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "moviecountry", ErrorMessageResourceType = typeof(Required))]
        public string Country { get; set; }

        [Display(Name = "movielanguage", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "movielanguage", ErrorMessageResourceType = typeof(Required))]

        public string Language { get; set; }

        [Display(Name = "movieuploaddate", ResourceType = typeof(Resource))]
        public DateTime Upload_Date { get; set; }

        [Display(Name = "movieproductiondate", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "movieprodate", ErrorMessageResourceType = typeof(Required))]

        public DateTime year_production { get; set; }



        [Required(ErrorMessageResourceName = "desc", ErrorMessageResourceType = typeof(Required))]
        [Display(Name = "moviedescription", ResourceType = typeof(Resource))]
        public string description { get; set; }

        [Display(Name = "movietype", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = "movietype", ErrorMessageResourceType = typeof(Required))]
        public int type_id { get; set; }




        [Display(Name = "publishername", ResourceType = typeof(Resource))]
        public string publisher_Name { get; set; }

        [Display(Name = "movietype", ResourceType = typeof(Resource))]
        public string Type_Name { get; set; }

        public int pulisher_id { get; set; }




        public string Trailer { get; set; }

        public string ImagePath { get; set; }

        public string CoverPath { get; set; }

        public string MovieDirectroy { get; set; }

        public List<string> ActorsNames { get; set; }

        public string Cast { set; get; }


        ////////////////////////
        ///

        public List<string> Countries = CountriesList();

        public static List<string> CountriesList()
        {
            List<String> CultureList = new List<string>();
            CultureInfo[] getcultureinfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo getculture in getcultureinfo)
            {
                RegionInfo getregioninfo = new RegionInfo(getculture.LCID);

                if (!(CultureList.Contains(getregioninfo.EnglishName)))
                {
                    CultureList.Add(getregioninfo.EnglishName);

                }

            }
            CultureList.Sort();
            return CultureList;



        }

        public List<string> Langs = Lang();
        private static List<string> Languages = new List<string>();
        private static CultureInfo[] cinfo = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);
        private static List<string> Lang()
        {


            foreach (CultureInfo culture in cinfo)
            {

                Languages.Add(culture.EnglishName);
            }
            return Languages;
        }
    }
}