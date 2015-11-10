using massey_effect_matthews_mages.DataAL;
using massey_effect_matthews_mages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace massey_effect_matthews_mages.Controllers
{
    public class WebController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Leaderboard()
        {
            IDAL DAL = new DBDataAL();
            IEnumerable<User> users = DAL.GetAllUsers();
            Leaderboard board = new Leaderboard();
            board.IsGlobal = true;
            board.Users = users;
            return View(board);
        }

        [HttpGet]
        public ActionResult Profile()
        {
			if (User.Identity.IsAuthenticated)
			{
				IDAL DAL = new DBDataAL();
				User u = DAL.GetUser(User.Identity.Name);

				return View("Profile", u);
			}
			else
			{
				return RedirectToAction("Login", "Membership");
			}
        }

        public ActionResult FriendProfile(string name)
        {
            IDAL DAL = new DBDataAL();
            User u = DAL.GetUser(name);

            return View("Profile", u);
        }


        [HttpGet]
        public ActionResult Play()
        {
            return View();            
        }

        [HttpGet]
        public ActionResult Store()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Crafting()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Messages()
        {
            return View();
        }

        //static pages
        [HttpGet]
        public ActionResult AboutUs()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Help()
        {
            return View();
        }


        [Authorize]
        [HttpGet]
        public ActionResult Friends()
        {
            string currentUser = User.Identity.Name;
            FriendData data = new FriendData(currentUser);
            return View(data);
        }

        [HttpGet]
        public ActionResult TempGame()
        {
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }
    }
}