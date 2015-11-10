using massey_effect_matthews_mages.DataAL;
using massey_effect_matthews_mages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace massey_effect_matthews_mages.Controllers
{
    [Authorize]
    public class LeaderboardController : Controller
    {
        [HttpGet]
        public ActionResult SwitchToGlobal()
        {
            Leaderboard board = new Leaderboard();
            board.IsGlobal = true;
            IDAL DAL = new DBDataAL();
            board.Users = DAL.GetAllUsers();
            return View("~/Views/Web/Leaderboard.cshtml", board);
        }

        [HttpGet]
        public ActionResult SwitchToFriends()
        {
            Leaderboard board = new Leaderboard();
            board.IsGlobal = false;
            IDAL DAL = new DBDataAL();
            User currentUser = DAL.GetUser(User.Identity.Name);
            IEnumerable<string> friends = DAL.GetFriends(currentUser);
            IEnumerable<User> users = DAL.GetAllUsers().Where(x => friends.Contains(x.UserName));
            return View("~/Views/Web/Leaderboard.cshtml", board);
        }
        
        [HttpPost]
        public ActionResult SortByNameAscending(bool isGlobal)
        {
            IDAL DAL = new DBDataAL();
            Leaderboard board = new Leaderboard();
            board.IsGlobal = isGlobal;
            IEnumerable<User> users;
            if (isGlobal)
            {
                users = DAL.GetAllUsers().OrderBy(x => x.UserName);
            }
            else
            {
                string userName = User.Identity.Name;
                User currentUser = DAL.GetUser(userName);
                IEnumerable<string> friends = DAL.GetFriends(currentUser);
                users = DAL.GetAllUsers().Where(x => friends.Contains(x.UserName)).OrderBy(x => x.UserName);
            }
            board.Users = users;
            return View("~/Views/Web/Leaderboard.cshtml", board);
        }

        [HttpPost]
        public ActionResult SortByNameDescending(bool isGlobal)
        {
            IDAL DAL = new DBDataAL();
            Leaderboard board = new Leaderboard();
            board.IsGlobal = isGlobal;
            IEnumerable<User> users;
            if (isGlobal)
            {
                users = DAL.GetAllUsers().OrderByDescending(x => x.UserName);
            }
            else
            {
                string userName = User.Identity.Name;
                User currentUser = DAL.GetUser(userName);
                IEnumerable<string> friends = DAL.GetFriends(currentUser);
                users = DAL.GetAllUsers().Where(x => friends.Contains(x.UserName)).OrderByDescending(x => x.UserName);
            }
            board.Users = users;
            return View("~/Views/Web/Leaderboard.cshtml", board);
        }

        [HttpPost]
        public ActionResult SortByMonstersKilledAscending(bool isGlobal)
        {
            IDAL DAL = new DBDataAL();
            Leaderboard board = new Leaderboard();
            board.IsGlobal = isGlobal;
            IEnumerable<User> users;
            if (isGlobal)
            {
                users = DAL.GetAllUsers().OrderBy(x => x.HighMonstersKilled);
            }
            else
            {
                string userName = User.Identity.Name;
                User currentUser = DAL.GetUser(userName);
                IEnumerable<string> friends = DAL.GetFriends(currentUser);
                users = DAL.GetAllUsers().Where(x => friends.Contains(x.UserName)).OrderBy(x => x.HighMonstersKilled);
            }
            board.Users = users;
            return View("~/Views/Web/Leaderboard.cshtml", board);
        }

        [HttpPost]
        public ActionResult SortByMonstersKilledDescending(bool isGlobal)
        {
            IDAL DAL = new DBDataAL();
            Leaderboard board = new Leaderboard();
            board.IsGlobal = isGlobal;
            IEnumerable<User> users;
            if (isGlobal)
            {
                users = DAL.GetAllUsers().OrderByDescending(x => x.HighMonstersKilled);
            }
            else
            {
                string userName = User.Identity.Name;
                User currentUser = DAL.GetUser(userName);
                IEnumerable<string> friends = DAL.GetFriends(currentUser);
                users = DAL.GetAllUsers().Where(x => friends.Contains(x.UserName)).OrderByDescending(x => x.HighMonstersKilled);
            }
            board.Users = users;
            return View("~/Views/Web/Leaderboard.cshtml", board);
        }

        [HttpPost]
        public ActionResult SortByRoomsTravelledAscending(bool isGlobal)
        {
            IDAL DAL = new DBDataAL();
            Leaderboard board = new Leaderboard();
            board.IsGlobal = isGlobal;
            IEnumerable<User> users;
            if (isGlobal)
            {
                users = DAL.GetAllUsers().OrderBy(x => x.HighRoomsTraveled);
            }
            else
            {
                string userName = User.Identity.Name;
                User currentUser = DAL.GetUser(userName);
                IEnumerable<string> friends = DAL.GetFriends(currentUser);
                users = DAL.GetAllUsers().Where(x => friends.Contains(x.UserName)).OrderBy(x => x.HighRoomsTraveled);
            }
            board.Users = users;
            return View("~/Views/Web/Leaderboard.cshtml", board);
        }

        [HttpPost]
        public ActionResult SortByRoomsTravelledDescending(bool isGlobal)
        {
            IDAL DAL = new DBDataAL();
            Leaderboard board = new Leaderboard();
            board.IsGlobal = isGlobal;
            IEnumerable<User> users;
            if (isGlobal)
            {
                users = DAL.GetAllUsers().OrderByDescending(x => x.HighRoomsTraveled);
            }
            else
            {
                string userName = User.Identity.Name;
                User currentUser = DAL.GetUser(userName);
                IEnumerable<string> friends = DAL.GetFriends(currentUser);
                users = DAL.GetAllUsers().Where(x => friends.Contains(x.UserName)).OrderByDescending(x => x.HighRoomsTraveled);
            }
            board.Users = users;
            return View("~/Views/Web/Leaderboard.cshtml", board);
        }
        
        [HttpPost]
        public ActionResult SortByAttackAscending(bool isGlobal)
        {
            IDAL DAL = new DBDataAL();
            Leaderboard board = new Leaderboard();
            board.IsGlobal = isGlobal;
            IEnumerable<User> users;
            if (isGlobal)
            {
                users = DAL.GetAllUsers().OrderBy(x => x.HighAttack);
            }
            else
            {
                string userName = User.Identity.Name;
                User currentUser = DAL.GetUser(userName);
                IEnumerable<string> friends = DAL.GetFriends(currentUser);
                users = DAL.GetAllUsers().Where(x => friends.Contains(x.UserName)).OrderBy(x => x.HighAttack);
                
            }
            board.Users = users;
            return View("~/Views/Web/Leaderboard.cshtml", board);
        }

        [HttpPost]
        public ActionResult SortByAttackDescending(bool isGlobal)
        {
            IDAL DAL = new DBDataAL();
            Leaderboard board = new Leaderboard();
            board.IsGlobal = isGlobal;
            IEnumerable<User> users;
            if (isGlobal)
            {
                users = DAL.GetAllUsers().OrderByDescending(x => x.HighAttack);
            }
            else
            {
                string userName = User.Identity.Name;
                User currentUser = DAL.GetUser(userName);
                IEnumerable<string> friends = DAL.GetFriends(currentUser);
                users = DAL.GetAllUsers().Where(x => friends.Contains(x.UserName)).OrderByDescending(x => x.HighAttack);

            }
            board.Users = users;
            return View("~/Views/Web/Leaderboard.cshtml", board);
        }

        [HttpPost]
        public ActionResult SortByDefenseAscending(bool isGlobal)
        {
            IDAL DAL = new DBDataAL();
            Leaderboard board = new Leaderboard();
            board.IsGlobal = isGlobal;
            IEnumerable<User> users;
            if (isGlobal)
            {
                users = DAL.GetAllUsers().OrderBy(x => x.HighDefense);
            }
            else
            {
                string userName = User.Identity.Name;
                User currentUser = DAL.GetUser(userName);
                IEnumerable<string> friends = DAL.GetFriends(currentUser);
                users = DAL.GetAllUsers().Where(x => friends.Contains(x.UserName)).OrderBy(x => x.HighDefense);

            }
            board.Users = users;
            return View("~/Views/Web/Leaderboard.cshtml", board);
        }

        [HttpPost]
        public ActionResult SortByDefenseDescending(bool isGlobal)
        {
            IDAL DAL = new DBDataAL();
            Leaderboard board = new Leaderboard();
            board.IsGlobal = isGlobal;
            IEnumerable<User> users;
            if (isGlobal)
            {
                users = DAL.GetAllUsers().OrderByDescending(x => x.HighDefense);
            }
            else
            {
                string userName = User.Identity.Name;
                User currentUser = DAL.GetUser(userName);
                IEnumerable<string> friends = DAL.GetFriends(currentUser);
                users = DAL.GetAllUsers().Where(x => friends.Contains(x.UserName)).OrderByDescending(x => x.HighDefense);

            }
            board.Users = users;
            return View("~/Views/Web/Leaderboard.cshtml", board);
        }
    }
}