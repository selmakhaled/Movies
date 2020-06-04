using Movies.Models;
using Movies.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movies.Controllers
{
    public class UserController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();


        // GET: User
        public ActionResult profile()
        {
            int x;
            x = (int)Session["UserId"];

            return View(db.USERSS.Find(x));
        }















        public ActionResult myProfile()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            UserProfileViewModel UPVM = new UserProfileViewModel();


            var s = (int)Session["UserId"];

            UPVM.user = db.USERSS.Find(s);
               var listOfFavourites = db.Favourite.Where(y => y.userId == s).ToList();
                List<Movie> lismov = new List<Movie>();
                foreach (var fav in listOfFavourites)
                {
                    Movie mov = db.MOVIESS.Where(r => r.Id == fav.movieId).Single();
                    if (mov != null)
                    {
                        lismov.Add(mov);
                    }

                }
                UPVM.Listfavouritesmovies = lismov;
                List<Admin> admins = new List<Admin>();

                UPVM.followinglist = db.Follow.Where(y => y.UserId == s).ToList();
                foreach (var follo in UPVM.followinglist)
                {
                    var c = db.ADMINSS.Where(t => t.Id == follo.PublisherId).FirstOrDefault();
                    admins.Add(c);


                }

                UPVM.publishersList = admins;





                List<Like_Dislike> likes = new List<Like_Dislike>();
                List<Movie> movies = new List<Movie>();

                likes = db.likes_Dislikes.Where(y => y.userId == s && y.isLike == true).ToList();
                foreach (var like in likes)
                {
                    var c = db.MOVIESS.Where(t => t.Id == like.movieId).FirstOrDefault();
                    movies.Add(c);


                }

                UPVM.likeList = movies;


                List<Movie> moviesOfFollwing = new List<Movie>();

                foreach (var fol in UPVM.followinglist)
                {


                    var k = db.MOVIESS.Where(z => z.pulisher_id == fol.PublisherId).ToList();

                    moviesOfFollwing.AddRange(k);
                }


                UPVM.follwingPublisherMovies = moviesOfFollwing;

        




            return View(UPVM);








        }







        public ActionResult passCheck(string pass)
        {
            int x;
            x = (int)Session["UserId"];
            var usr = db.USERSS.Find(x);
            if (usr != null)
            {


                if (pass == usr.Password) return RedirectToAction("editAccount", "User");
                else return Json(0);
            }


            return Json(0);

        }





















        [HttpGet]
        public ActionResult editAccount(int Id)
        {

            if (Session["UserId"] != null)
            {

                USER user = new USER();
                user = db.USERSS.Find(Id);
                user.ConfirmPassword = user.Password;



                return View(user);









            }
            else { return RedirectToAction("Login", new { Controller = "Home" }); }


        }

        [HttpPost]
        public ActionResult editAccount(USER EAVM, HttpPostedFileBase file3)

        {

            var usr = db.USERSS.Where(y => y.Id == EAVM.Id).FirstOrDefault();

            if (EAVM.currentPassword != usr.Password)
            {
                return View(EAVM);
            }




            if (file3 != null)
            {
                string path3 = Server.MapPath("~/Profile_Images/");
                string fileName3 = Path.GetFileName(file3.FileName);
                string fullpath3 = Path.Combine(path3, fileName3);
                file3.SaveAs(fullpath3);
                usr.ImagePath = fileName3;
            }

            //List<ModelError> v = new List<ModelError>() ;
            //foreach (ModelState modelState in ViewData.ModelState.Values)
            //{
            //    foreach (ModelError error in modelState.Errors)
            //    {
            //        v.Add(error);
            //    }
            //}
            usr.ImagePath = EAVM.ImagePath;
            usr.Name = EAVM.Name;
            usr.Password = EAVM.Password;
            usr.ConfirmPassword = EAVM.Password;
            usr.Email = EAVM.Email;





            if (ModelState.IsValid)
            {




                db.Entry(usr).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("myprofile", new { controller = "User" });
            }
            else { return View(EAVM); }



        }

    }
}
