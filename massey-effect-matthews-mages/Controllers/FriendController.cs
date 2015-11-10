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
    public class FriendController : Controller
    {
        [HttpGet]
        public ActionResult ViewMessage(int messageId)
        {
            IDAL DAL = new DBDataAL();
            string Name = User.Identity.Name;
            User user = DAL.GetUser(Name);
            Message message = DAL.GetMessagesFor(user).Where(m => m.Id == messageId).ToList().First();
            return View("Message", message);
        }

        [HttpPost]
        public ActionResult DeleteMessage(int messageId)
        {
            IDAL DAL = new DBDataAL();
            string Name = User.Identity.Name;
            DAL.RemoveMessage(messageId);

            FriendData data = new FriendData(Name);
            return View("~/Views/Web/Friends.cshtml", data);
        }

        [HttpPost]
        public ActionResult SendMessage(Message message)
        {
            IDAL DAL = new DBDataAL();
            DAL.CreateMessage(message);

            string Name = User.Identity.Name;
            FriendData data = new FriendData(Name);
            return View("~/Views/Web/Friends.cshtml", data);
        }

        [HttpPost]
        public ActionResult NewMessage(string friend)
        {
            IDAL DAL = new DBDataAL();
            DateTime dt = DateTime.Now;
            byte[] bytes = BitConverter.GetBytes(dt.Ticks);
            Message message = new Message();
            message.Sender = User.Identity.Name;
            message.Receiver = friend;
            message.TimeStamp = bytes;
            return View(message);
        }

        //view friend profile

        [HttpPost]
        public ActionResult RemoveFriend(string friend)
        {
            string Name = User.Identity.Name;

            IDAL DAL = new DBDataAL();
            DAL.RemoveFriend(Name, friend);

            FriendData data = new FriendData(Name);
            return View("~/Views/Web/Friends.cshtml", data);
        }

        [HttpPost]
        public ActionResult AcceptFriendRequest(string sender)
        {
            string currentUser = User.Identity.Name;
            IDAL DAL = new DBDataAL();

            User user = DAL.GetUser(currentUser);

            IEnumerable<string> friends = DAL.GetFriends(user).ToList();
            if(!friends.Contains(sender))
            {
                DAL.AddFriend(currentUser, sender);
            }

            IEnumerable<FriendRequest> requests = DAL.GetFriendRequests(user).Where(r => r.Sender == sender);
            IEnumerable<FriendRequest> requests2 = DAL.GetFriendRequests(DAL.GetUser(sender)).Where(r => r.Sender == currentUser);
            IEnumerable<FriendRequest> fullList = requests.Concat(requests2);
            foreach (FriendRequest req in fullList)
            {
                DAL.RemoveFriendRequest(req);
            }

            FriendData data = new FriendData(currentUser);
            return View("~/Views/Web/Friends.cshtml", data);
        }

        [HttpPost]
        public ActionResult DenyFriendRequest(string sender)
        {
            string currentUser = User.Identity.Name;

            IDAL DAL = new DBDataAL();
            User user = DAL.GetUser(currentUser);
            FriendRequest fr = DAL.GetFriendRequests(user).Where(r => r.Sender == sender).ToList().First();
            DAL.RemoveFriendRequest(fr);
            
            FriendData data = new FriendData(currentUser);
            return View("~/Views/Web/Friends.cshtml", data);
        }
        
        [HttpGet]
        public ActionResult Search(string name)
        {
            string currentUser = User.Identity.Name;

            FriendData data = new FriendData(currentUser);
            data.SearchFor(name);

            return View("~/Views/Web/Friends.cshtml", data);
        }

        [HttpPost]
        public ActionResult RequestFriend(string name)
        {
            string currentUser = User.Identity.Name;

            IDAL DAL = new DBDataAL();
            User user = DAL.GetUser(name);
            try
            {
                FriendRequest requests = DAL.GetFriendRequests(user).Where(x => x.Sender == currentUser).First();
            }
            catch(Exception) //if null catch explosion
            {
                FriendRequest fr = new FriendRequest();
                fr.Receiver = name;
                fr.Sender = currentUser;
                DAL.CreateFriendRequest(fr);
            }

            FriendData data = new FriendData(currentUser);

            return View("~/Views/Web/Friends.cshtml", data);
        }
    }
}