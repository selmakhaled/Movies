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

    }
}