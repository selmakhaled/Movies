using Movies.Models;
using Movies.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.Web.Script.Serialization;


namespace Movies.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult about()
        {

            return View();

        }




        [HttpGet]
        public ActionResult forgotpassword()
        {
            return View();




        }


        [HttpPost]
        public ActionResult forgotpassword(string Email)
        {
            var user = db.USERSS.Where(y => y.Email == Email).FirstOrDefault();
            if (user == null)
            {
                return Json("This Email Is Not Exist !!!");

            }
            else
            {
                try
                {

                    string strPwdchar = "abcdefghijklmnopqrstuvwxyz0123456789#+@&$ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    string strPwd = "";
                    Random rnd = new Random();
                    for (int i = 0; i <= 7; i++)
                    {
                        int iRandom = rnd.Next(0, strPwdchar.Length - 1);
                        strPwd += strPwdchar.Substring(iRandom, 1);
                    }
                    user.Password = strPwd;
                    user.ConfirmPassword = strPwd;
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;

                    db.SaveChanges();

                    EmailManager.SendEmail("Reset Password", "Movies Land Website " + " Hello  " + user.Name + "  Your New Password is : " + strPwd, user.Email);



                    return Json("An email Was sent to you check your inbox ");
                }
                catch (Exception)
                {

                    //Edit that user with new password

                    return Json("Failed to reset Try another Time.");
                }



            }

        }










        // GET: Home
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]

        public ActionResult Login(Login user)
        {
            if (ModelState.IsValid)
            {
                Session["Admin"] = null;
                AdminUserViewModel auvm = new AdminUserViewModel();
                auvm.admin = db.ADMINSS.Where(y => y.Email == user.Email && y.Password == user.Password).FirstOrDefault();
                auvm.user = db.USERSS.Where(y => y.Email == user.Email & y.Password == user.Password).FirstOrDefault();

                if (auvm.admin != null)
                {
                    Session["UserId"] = auvm.admin.Id;
                    Session["UserName"] = auvm.admin.Name;
                    Session["Admin"] = 1;

                    return RedirectToAction("Index", "Admin");

                }
                else
                {

                    if (auvm.user != null)
                    {
                        Session["userimg"] = auvm.user.ImagePath;
                        Session["UserId"] = auvm.user.Id;
                        Session["UserName"] = auvm.user.Name;
                        Session["Admin"] = null;
                        return RedirectToAction("Index", "Home");



                    }
                    else return View();

                }

            }
            else return View();




        }





        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserViewModel Vmodel, HttpPostedFileBase file3)
        {
            if (file3 != null)
            {

                string path = Server.MapPath("~/Profile_Images/");
                string fileName = Path.GetFileName(file3.FileName);
                string fullpath = Path.Combine(path, fileName);
                file3.SaveAs(fullpath);
                Vmodel.ImagePath = fileName;

            }
            if (db.USERSS.Where(y => y.Email == Vmodel.Email).FirstOrDefault() != null) { Vmodel.Email = ""; return View(Vmodel); }
            if (Vmodel.Password.Count() < 8) { Vmodel.Password = null; }
            if (ModelState.IsValid)
            {
                USER user = new USER();
                user.Password = Vmodel.Password;
                user.Name = Vmodel.Name;
                user.ImagePath = Vmodel.ImagePath;
                user.Id = Vmodel.Id;
                user.Email = Vmodel.Email;



                db.USERSS.Add(user);
                db.SaveChanges();


                return RedirectToAction("Login", "Home");
            }
            else return View(Vmodel);
        }

        [HttpPost]
        public ActionResult emailcheck(string Email)
        {

            var user = db.USERSS.Where(t => t.Email == Email).FirstOrDefault();
            if (user != null) { return Json(1); }
            else return Json(0);
        }




        public ActionResult Logout()
        {
            Session["UserId"] = null;
            Session["Admin"] = null;
            Session["MovieId"] = null;
            Session["UserName"] = null;
            Session["userimg"] = null;


            return RedirectToAction("Login", "Home");
        }




        [HttpGet]
        public ActionResult Index()
        {
            MoviesList_movieseach MLMS = new MoviesList_movieseach();

            if (Session["UserId"] != null)
            {


                MLMS.movies = db.MOVIESS.ToList();
                MLMS.movies.Reverse();
                MLMS.Types = db.TYPESS.ToList();
                return View(MLMS);
            }
            else return RedirectToAction("Login");

        }


        [HttpPost]
        public ActionResult Index(MoviesList_movieseach MLMS)
        {
            var moviesFromDb = db.MOVIESS.ToList();
            if (MLMS.Country == null && MLMS.Language == null && MLMS.Name == null && MLMS.type_id == null && MLMS.year_production == null)
            {
                MLMS.movies = moviesFromDb;

                return View(MLMS);
            }
            else
            {


                List<Movie> Matched_Movies = new List<Movie>();


                if (MLMS.Name != null)
                {
                    Matched_Movies = db.MOVIESS.Where(y => y.Name.Contains(MLMS.Name)).ToList();

                }
                else Matched_Movies = db.MOVIESS.ToList();


                if (MLMS.type_id != null)
                {
                    Matched_Movies = Matched_Movies.Where(y => y.type_id == MLMS.type_id).ToList();


                }

                if (MLMS.Language != null)
                {
                    Matched_Movies = Matched_Movies.Where(y => y.Language == MLMS.Language).ToList();

                }

                if (MLMS.year_production != null)
                {
                    Matched_Movies = Matched_Movies.Where(y => y.year_production.Year == MLMS.year_production).ToList();


                }

                //if (movie_search.type_id != null)
                //{
                //    var movies = db.MOVIESS.Where(y => y.type_id == movie_search.type_id).ToList();
                //    foreach (var movie in movies)
                //        Matched_Movies.Add(movie);

                //}




                //foreach (var movie in Matched_Movies)
                //{







                //}




                //List<Movie> matched = new List<Movie>();
                //foreach (var match in Matched_Movies)
                //{
                //    if (MLMS.Name !=null && MLMS.year_production != null&& MLMS.type_id== null&&MLMS.Country==null&&MLMS.Language==null)
                //    {
                //        var movies = db.MOVIESS.Where(y => y.Name.Contains( MLMS.Name) && y.year_production.Year == MLMS.year_production).ToList();
                //    }


                //}
                MLMS.movies = Matched_Movies;

                MLMS.Types = db.TYPESS.ToList();
                return View(MLMS);
            }





        }




    }
}
