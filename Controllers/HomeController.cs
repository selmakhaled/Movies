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




        public ActionResult Index()
        {
            return View();
        }




    }
}
