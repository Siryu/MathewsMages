using massey_effect_matthews_mages.DataAL;
using massey_effect_matthews_mages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace massey_effect_matthews_mages.Controllers
{
	[Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
		[HttpGet]
        public ActionResult EditUser(string id)
        {
			IDAL dal = new DBDataAL();
			string userName = id;
			userName = userName ?? "";
			bool requestHasError = false;
			User user = dal.GetUser(userName);

			if (dal.isInRole(userName, "Admin")
				&& User.Identity.Name != userName)
			{
				ViewBag.AdminError = userName + " is an admin and you are currently logged in as " + User.Identity.Name
					+ ". To edit this user, please log in to that account.";
				requestHasError = true;
			}
			else if (user == null)
			{
				ViewBag.AdminError = "User was not found";
				requestHasError = true;
			}

			ActionResult view = null;
			if(!requestHasError)
				view = View(new UserViewModel(user));
			else
				view = View("~/Views/Admin/AdminError.cshtml");


            return view;
        }

		[HttpPost]
		public ActionResult EditUser(string id, UserViewModel viewModel)
		{
			string userName = id;
			IDAL dal = new DBDataAL();
			User user = dal.GetUser(userName);

			if (user != null)
			{
				user.Attack = viewModel.Attack;
				//user.AttackRange = viewModel.AttackRange;
				//user.AttackSpeed = viewModel.AttackSpeed;
				user.CurrentHealth = viewModel.CurrentHealth;
				user.Defense = viewModel.Defense;
				user.HighMonstersKilled = viewModel.MonstersKilled;
				//user.MoveSpeed = viewModel.MoveSpeed;
				user.HighRoomsTraveled = viewModel.RoomsTraveled;
				user.HighDefense = viewModel.HighestDefenseAchieved;
				user.HighAttack = viewModel.HighestAttackAchieved;

				dal.UpdateUser(user);
			}

			return EditUser(userName);
		}

		public ActionResult Users(string searchCriteria)
		{
			searchCriteria =  searchCriteria ?? "";
			IDAL dal = new DBDataAL();
			IEnumerable<LogUser> logUsers = dal.GetAllLogUsers().Where(u => u.UserName.Contains(searchCriteria));
			return View("~/Views/Admin/Users.cshtml", logUsers);
		}

		[HttpPost]
		public ActionResult DeleteUser(string id)
		{
			string userName = id;
			IDAL dal = new DBDataAL();
			dal.DeleteUser(userName);
			return RedirectToAction("Users");
		}
    }
}