using Movies.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Movies.ViewModels
{
    public class MoviesList_movieseach
    {

        public List<Movie> movies = new List<Movie>();

        //public  SearchMovie Searched = new SearchMovie();

        public List<TypeOfMovie> Types = new List<TypeOfMovie>();




        [DataType(DataType.Text)]
        [Display(Name = "moviename", ResourceType = typeof(Resource))]
        public string Name { get; set; }



        [DataType(DataType.Text)]
        [Display(Name = "moviecountry", ResourceType = typeof(Resource))]
        public string Country { get; set; }

        [Display(Name = "movielanguage", ResourceType = typeof(Resource))]
        [DataType(DataType.Text)]
        public string Language { get; set; }




        [Display(Name = "movieproductiondate", ResourceType = typeof(Resource))]
        public int? year_production { get; set; }



        [DataType(DataType.Text)]
        [Display(Name = "movietype", ResourceType = typeof(Resource))]
        public int? type_id { get; set; }


        //
        public List<string> Langs = Lang();

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