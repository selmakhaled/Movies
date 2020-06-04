using Movies.Models;
using Movies.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movies.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {


            if (Session["UserId"] != null && Session["Admin"] != null)
            {
                return View();
            }

            else { return RedirectToAction("Login", "Home"); }



        }


        public ActionResult ShowAllActors()
        {
            if (Session["UserId"] == null) { RedirectToAction("Login", new { Controller = "Home" }); }

            return View(db.Actors.ToList());


        }

        [HttpGet]
        public ActionResult AddNewActor()
        {
            if (Session["UserId"] == null) { RedirectToAction("Login", new { Controller = "Home" }); }
            Actor actor = new Actor();
            actor.Countries = actor.CountriesList();
            return View(actor);
        }


        [HttpPost]
        public ActionResult AddNewActor(Actor actor, HttpPostedFileBase imgPath)
        {

            ////
            ///
            if (imgPath != null)
            {
                string path = Server.MapPath("~/Actors/");
                string fileName = Path.GetFileName(imgPath.FileName);
                string fullpath = Path.Combine(path, fileName);
                imgPath.SaveAs(fullpath);
                actor.imgPath = fileName;
            }
            else
            {


                actor.imgPath = "default.jpg";

            }

            if (ModelState.IsValid)
            {
                db.Actors.Add(actor);
                db.SaveChanges();
                return RedirectToAction("Index");


            }
            else
            {
                actor.Countries = actor.CountriesList();
                return View(actor);
            }



        }
        [HttpGet]
        public ActionResult EditActor(int Id)
        {
            // search then pass to view
            if (Session["UserId"] == null) { RedirectToAction("Login", new { Controller = "Home" }); }

            var actor = db.Actors.Find(Id);

            actor.Countries = actor.CountriesList();

            return View(actor);



        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult EditActor(Actor actor, HttpPostedFileBase imgPath)
        {
            // save in database

            if (imgPath != null)
            {
                string path = Server.MapPath("~/Actors/");
                string fileName = Path.GetFileName(imgPath.FileName);
                string fullpath = Path.Combine(path, fileName);
                imgPath.SaveAs(fullpath);
                actor.imgPath = fileName;
            }


            if (ModelState.IsValid)
            {
                db.Entry(actor).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("ShowAllActors", "Admin");

            }
            else
            {
                actor.Countries = actor.CountriesList();
                return View(actor);
            }
        }




        public ActionResult DeleteActor(int Id)
        {
            if (Session["UserId"] == null) { RedirectToAction("Login", new { Controller = "Home" }); }

            var actor = db.Actors.Where(y => y.Id == Id).FirstOrDefault();

            db.Actors.Remove(actor);
            db.SaveChanges();
            return RedirectToAction("ShowAllActors");

        }




        //view model movies +actor
        [HttpGet]
        public ActionResult DetailsActor(int Id)
        {
            if (Session["UserId"] == null) { RedirectToAction("Login", new { Controller = "Home" }); }
            var actor = db.Actors.Where(t => t.Id == Id).FirstOrDefault();


            return View(actor);

        }

        [HttpGet]
        public ActionResult ActorDetails(int Id)
        {
            if (Session["UserId"] == null) { RedirectToAction("Login", new { Controller = "Home" }); }

            Actor actor = db.Actors.Find(Id);


            if (actor != null)
            {
                ActorMoviesViewModel AMVM = new ActorMoviesViewModel();

                AMVM.actor = actor;

                List<Movie> MoviesList = db.MOVIESS.Where(y => y.Cast.Contains(actor.full_name.ToLower())).ToList();

                AMVM.ActorMovies = MoviesList;

                return View(AMVM);

            }
            else return RedirectToAction("Index", "Home");




        }

        public ActionResult mymovies()
        {
            int x;
            x = (int)Session["UserId"];

            var Mymovieslist = db.MOVIESS.Where(p => p.pulisher_id == x).ToList();
            return View("showallmovies", "Admin", Mymovieslist);

        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult AddNewMovie()
        {
            if (Session["UserId"] == null) { RedirectToAction("Login", new { Controller = "Home" }); }
            // get types from database and add to X.types

            MovieTypesViewModel x = new MovieTypesViewModel();
            var Actors = db.Actors.ToList();
            List<string> actnames = new List<string>();
            string lowername;
            foreach (Actor actor in Actors)
            {
                lowername = actor.full_name.ToLower();
                actnames.Add(lowername);


            }
            x.ActorsNames = actnames;

            x.TypesList = db.TYPESS.ToList();
            return View(x);

        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]

        public ActionResult AddNewMovie(MovieTypesViewModel s, HttpPostedFileBase file, HttpPostedFileBase file2, HttpPostedFileBase file3, HttpPostedFileBase file4)

        {

            if (file != null && file2 != null && file3 != null/*&&file4 != null*/)
            {
                string extension = Path.GetExtension(file.FileName).ToLower();
                string extension2 = Path.GetExtension(file2.FileName).ToLower();
                if (extension != ".png" && extension != ".jpg") { return Content("please upload jpg or png..."); }
                if (extension2 != ".mp4" && extension2 != ".flv" && extension2 != ".mkv" && extension2 != ".wmv") { return Content("please upload Mp4 or FLV..."); }

                //image upload

                ////
                string path = Server.MapPath("~/Images/");
                string fileName = Path.GetFileName(file.FileName);
                string fullpath = Path.Combine(path, fileName);
                file.SaveAs(fullpath);
                s.ImagePath = fileName;


                ///
                ///video upload 
                string path2 = Server.MapPath("~/Videos/");
                string fileName2 = Path.GetFileName(file2.FileName);
                string fullpath2 = Path.Combine(path2, fileName2);
                file2.SaveAs(fullpath2);
                s.MovieDirectroy = fileName2;
                /// // / ////////////////////////////////////////////////////
                /// /// ///////////





                if (file4 != null)
                {
                    string path4 = Server.MapPath("~/Trailers/");
                    string fileName4 = Path.GetFileName(file4.FileName);
                    string fullpath4 = Path.Combine(path4, fileName4);
                    file4.SaveAs(fullpath4);
                    s.Trailer = fileName4;

                }





                /// 
                /// 
                /// 


                string path3 = Server.MapPath("~/Images/Covers/");
                string fileName3 = Path.GetFileName(file3.FileName);
                string fullpath3 = Path.Combine(path3, fileName3);

                file3.SaveAs(fullpath3);


                s.CoverPath = fileName3;




                /// /////////////////////////////////////
                s.Upload_Date = DateTime.Now.Date;
                int x = (int)Session["UserId"];
                s.pulisher_id = x;
            }






            s.TypesList = db.TYPESS.ToList();
            var viewmodel = s;
            foreach (var type in db.TYPESS.ToList())
            {
                if (s.type_id == type.Id)
                {
                    s.Type_Name = type.type_name;
                }
            }

            foreach (var admin in db.ADMINSS.ToList())
            {
                if (s.pulisher_id == admin.Id) { s.publisher_Name = admin.Name; }
            }



            Movie movie = new Movie();
            movie.Name = s.Name;
            movie.Country = s.Country;
            movie.description = s.description;
            movie.Duration = s.Duration;
            movie.Id = s.Id;
            movie.ImagePath = s.ImagePath;
            movie.Language = s.Language;
            movie.MovieDirectroy = s.MovieDirectroy;
            movie.publisher_Name = s.publisher_Name;
            movie.pulisher_id = s.pulisher_id;
            movie.type_id = s.type_id;
            movie.Type_Name = s.Type_Name;
            movie.Upload_Date = s.Upload_Date;
            movie.year_production = s.year_production;
            movie.CoverPath = s.CoverPath;
            movie.Trailer = s.Trailer;
            movie.Cast = s.Cast;


            //var listofActor = s.Cast.Split(',');

            // foreach(String actr in listofActor) {

            //   var actorDB = db.Actors.Where(r => r.full_name == actr).FirstOrDefault();

            // }


            /// //// /////////////////////////////////////////

            // //// //////////////////////////////////////////////////////////


            if (ModelState.IsValid)
            {




                db.MOVIESS.Add(movie);
                db.SaveChanges();
                Session["MovieId"] = s.Id;
                //var   listofFollowers =    db.Follow.Where(y => y.PublisherId == s.pulisher_id).ToList();
                // foreach (var follower in listofFollowers )
                //           {
                //               var userEmail = db.USERSS.Find(follower.UserId);
                //               EmailManager.SendEmail("<b>Movies land</b>A New Movie has been uploaded ",s.Name+s.year_production.Year+" Was uploaded recently By: "+s.publisher_Name,userEmail.Email,s.ImagePath);

                //           }



                return RedirectToAction("AddNewMovie", "Admin");

            }
            else
            {
                //List<ModelError> c = new List<ModelError>();
                s.TypesList = db.TYPESS.ToList();
                return View(s);
                //foreach (ModelState modelState in ModelState.Values)
                //{
                //    foreach (ModelError error in modelState.Errors)
                //    {
                //        c.Add(error);
                //    }
                //}
                //return Content(c.ToString());


            }


        }









        //
        public ActionResult showallmovies()
        {




            if (Session["UserId"] != null)
            {
                return View(db.MOVIESS.ToList());


            }
            else
            {
                return RedirectToAction("Login", "Home");

            }








        }


        /// ////////////////////////////////////////////////////////////
        /// //////////////////////////MOVIE/////////////////////////////
        /// ////////////////////////////////////////////////////////////


        [HttpGet]
        public ActionResult editMovie(int Id)
        {
            if (Session["UserId"] == null) { RedirectToAction("Login", new { Controller = "Home" }); }
            // Search in database by id and pass result to view
            // Types 

            var movie = db.MOVIESS.Find(Id);

            EditMovieViewModel MTVM = new EditMovieViewModel();

            MTVM.TypesList = db.TYPESS.ToList();
            MTVM.Country = movie.Country;
            MTVM.description = movie.description;
            MTVM.Duration = movie.Duration;
            MTVM.Id = movie.Id;
            MTVM.ImagePath = movie.ImagePath;
            MTVM.Language = movie.Language;
            MTVM.MovieDirectroy = movie.MovieDirectroy;
            MTVM.Name = movie.Name;
            MTVM.publisher_Name = movie.publisher_Name;
            MTVM.pulisher_id = movie.pulisher_id;
            MTVM.type_id = movie.type_id;
            MTVM.Type_Name = movie.Type_Name;
            MTVM.Upload_Date = movie.Upload_Date;
            MTVM.year_production = movie.year_production;
            MTVM.CoverPath = movie.CoverPath;
            MTVM.Trailer = movie.Trailer;


            var Actors = db.Actors.ToList();
            List<string> actnames = new List<string>();
            string lowername;
            foreach (Actor actor in Actors)
            {
                lowername = actor.full_name.ToLower();
                actnames.Add(lowername);


            }
            MTVM.actorslist = actnames;





            Session["publisher"] = movie.publisher_Name;
            Session["UploadDate"] = movie.Upload_Date;





            return View(MTVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult editMovie(EditMovieViewModel s, HttpPostedFileBase file, HttpPostedFileBase file2, HttpPostedFileBase file3, HttpPostedFileBase file4)
        {
            s.publisher_Name = (string)Session["publisher"];
            s.Upload_Date = (DateTime)Session["UploadDate"];


            s.TypesList = db.TYPESS.ToList();
            foreach (var type in db.TYPESS.ToList())
            {
                if (s.type_id == type.Id)
                {
                    s.Type_Name = type.type_name;
                }
            }


            if (file4 != null)
            {
                string path4 = Server.MapPath("~/Trailers/");
                string fileName4 = Path.GetFileName(file4.FileName);
                string fullpath4 = Path.Combine(path4, fileName4);
                file4.SaveAs(fullpath4);
                s.Trailer = fileName4;


            }

            if (file != null)
            {
                string path = Server.MapPath("~/Images/");
                string fileName = Path.GetFileName(file.FileName);
                string fullpath = Path.Combine(path, fileName);
                file.SaveAs(fullpath);
                s.ImagePath = fileName;


            }
            if (file2 != null)
            {
                ///
                ///video upload 
                string path2 = Server.MapPath("~/Videos/");
                string fileName2 = Path.GetFileName(file2.FileName);
                string fullpath2 = Path.Combine(path2, fileName2);
                file2.SaveAs(fullpath2);
                s.MovieDirectroy = fileName2;
            }


            if (file3 != null)
            {
                ///

                string path3 = Server.MapPath("~/Images/Covers/");
                string fileName3 = Path.GetFileName(file3.FileName);
                string fullpath3 = Path.Combine(path3, fileName3);
                file3.SaveAs(fullpath3);
                s.CoverPath = fileName3;

            }



            if (ModelState.IsValid)
            {
                Movie movie = new Movie();
                movie.Name = s.Name;
                movie.Country = s.Country;
                movie.description = s.description;
                movie.Duration = s.Duration;
                movie.Id = s.Id;
                movie.ImagePath = s.ImagePath;
                movie.Language = s.Language;
                movie.MovieDirectroy = s.MovieDirectroy;
                movie.publisher_Name = s.publisher_Name;
                movie.pulisher_id = s.pulisher_id;
                movie.type_id = s.type_id;
                movie.Type_Name = s.Type_Name;
                movie.Upload_Date = s.Upload_Date;
                movie.year_production = s.year_production;
                movie.CoverPath = s.CoverPath;
                movie.Trailer = s.Trailer;
                movie.Cast = s.Cast;

                //save movie after editing in the database
                db.Entry(movie).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("showallmovies", "Admin");

            }//else if not vaild 
            return View();




        }




        public ActionResult deleteMovie(int Id)
        {

            var c = db.MOVIESS.Find(Id);
            string fullPath = Request.MapPath("~/Images/" + c.ImagePath);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }


            fullPath = Request.MapPath("~/Videos/" + c.MovieDirectroy);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            db.MOVIESS.Remove(c);
            db.SaveChanges();

            return RedirectToAction("showallmovies", "Admin");

        }


        //
        //
        //
        //
        //
        //
        //

        public ActionResult detailsMovie(int Id)
        {

            var movie = db.MOVIESS.Find(Id);

            return View(movie);

        }



        [HttpGet]
        public ActionResult addmovietype()
        {
            if (Session["UserId"] == null) { RedirectToAction("Login", new { Controller = "Home" }); }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addmovietype(TypeOfMovie x)
        {
            if (ModelState.IsValid)
            {
                // save in database
                db.TYPESS.Add(x);
                db.SaveChanges();

                return RedirectToAction("showallmovies", "Admin");
            }
            else return View();

        }

        public ActionResult showallusers()
        {
            if (Session["UserId"] == null) { RedirectToAction("Login", new { Controller = "Home" }); }
            var users = db.USERSS.ToList();

            // get all users and pass to view




            return View(users);
        }

        public ActionResult deleteUser(int Id)
        {

            if (Session["Admin"] != null)
            {
                var c = db.USERSS.Find(Id);
                db.USERSS.Remove(c);
                db.SaveChanges();
                return RedirectToAction("showallusers", "Admin");
            }
            else
            {
                Session["UserId"] = null;
                Session["Admin"] = null;
                Session["MovieId"] = null;
                Session["UserName"] = null;

                return RedirectToAction("Login", "Home");
            }
        }





        [AllowAnonymous]
        [HttpGet]
        public ActionResult editUser(int Id)
        {
            // search then pass to view


            USER user = new USER();
            user = db.USERSS.Find(Id);
            user.ConfirmPassword = user.Password;



            return View(user);
        }

        [HttpPost]

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult editUser(USER x, HttpPostedFileBase file3)
        {



            // save in database
            if (file3 != null)
            {
                string path3 = Server.MapPath("~/Profile_Images/");


                string fileName3 = Path.GetFileName(file3.FileName);
                string fullpath3 = Path.Combine(path3, fileName3);
                file3.SaveAs(fullpath3);
                x.ImagePath = fileName3;


            }
            //ModelState.isvaild();


            //  ModelState.Remove();
            //if (ModelState.ContainsKey("{0}"))
            //      ModelState["{0}"].Errors.Clear();

            if (ModelState.IsValid)
            {

                db.Entry(x).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("showallusers", "Admin");
            }
            else
            {
                //List<ModelError> c = new List<ModelError>();
                //foreach(ModelState modelState in ViewData.ModelState.Values) {
                //    foreach (ModelError error in modelState.Errors)
                //    {
                //        c.Add(error);
                //    }
                //}


                return View(x);
            }






        }








        public ActionResult detailsUser(int? Id)
        {


            var user = db.USERSS.Find(Id);

            return View(user);
        }


        public ActionResult showalladmins()
        {
            var admins = db.ADMINSS.ToList();
            return View(admins);


        }











    }
}
 
