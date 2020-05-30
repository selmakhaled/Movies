using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Movies.Models
{
    public class Actor
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = "Actname", ErrorMessageResourceType = typeof(Required))]
        [Display(Name = "username", ResourceType = typeof(Resource))]
        public string full_name { get; set; }

        public string Summary { get; set; }


        //name in movie
        [Display(Name = "ImgActor", ResourceType = typeof(Resource))]
        [DataType(DataType.Upload)]
        public string imgPath { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime DateOfBirth { get; set; }

        public string PlaceOfBirth { get; set; }

        [NotMapped]
        public List<string> Countries { set; get; }

        public List<string> CountriesList()
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

    }
}