using massey_effect_matthews_mages.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace massey_effect_matthews_mages.DataAL
{
    public class DBDataAL : IDAL
    {
        public void UpdateUser(Models.User user)
        {
            using (TempDBContext1 db = new TempDBContext1())
            {
                User oldUser = db.Users.Find(user.UserName);
                oldUser.CurrentHealth = user.CurrentHealth; 
                oldUser.MonstersKilled = user.MonstersKilled;
                oldUser.RoomsTraveled = user.RoomsTraveled;
                oldUser.Attack = user.Attack;
                oldUser.Defense = user.Defense;
                oldUser.AttackSpeed = user.AttackSpeed;
                oldUser.AttackRange = user.AttackRange;
                oldUser.MoveSpeed = user.MoveSpeed;
                oldUser.HighAttack = user.HighAttack;
                oldUser.HighDefense = user.HighDefense;
                oldUser.HighMonstersKilled = user.HighMonstersKilled;
                oldUser.HighRoomsTraveled = user.HighRoomsTraveled;
                db.SaveChanges();
            }
        }

        public void CreateUser(Models.User user)
        {
            using(TempDBContext1 db = new TempDBContext1())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public Models.User GetUser(string userName)
        {
            User foundUser = null;
            using (TempDBContext1 db = new TempDBContext1())
            {
				foundUser = db.Users.Where(u => u.UserName == userName).FirstOrDefault();
				//foundUser = db.Users.Find(userName);
            }
            return foundUser;
        }

		public bool isInRole(string userName, string roleName)
		{
			bool isInRole = false;
			using(TempDBLogUsers db = new TempDBLogUsers())
			{
				LogUser user = db.LogUsers.Where(u => u.UserName == userName).FirstOrDefault();
				if(user != null)
					isInRole = user.Roles.Where(r => r.Name == roleName).Any();
			}
			return isInRole;
		}

		public LogUser GetLogUser(string userName)
		{
			LogUser user = null;
			using(TempDBLogUsers db = new TempDBLogUsers())
			{
				user = db.LogUsers.Where(u => u.UserName == userName).FirstOrDefault();
			}

			return user;
		}

		public void DeleteUser(string userName)
		{
			//Remove user from Friend table
			using(SqlDBFriendListContext db = new SqlDBFriendListContext())
			{
				List<Friend> friends = db.Friends.Where(u => (u.Friend1 == userName) || (u.User == userName)).ToList();
				
				foreach(Friend friend in friends)
				{
					db.Friends.Remove(friend);
					db.Entry(friend).State = EntityState.Deleted;
				}
				db.SaveChanges();
			}

			//Remove user from FriendRequest and Message table
			using(TempDBFriendContext db = new TempDBFriendContext())
			{
				List<FriendRequest> friends = db.FriendRequests.Where(u => (u.Sender == userName) || (u.Receiver == userName)).ToList();
				List<Message> messages = db.Messages.Where(u => (u.Sender == userName) || (u.Receiver == userName)).ToList();

				foreach (FriendRequest friend in friends)
				{
					db.FriendRequests.Remove(friend);
					db.Entry(friend).State = EntityState.Deleted;
				}
				foreach(Message m in messages)
				{
					db.Messages.Remove(m);
					db.Entry(m).State = EntityState.Deleted;
				}

				db.SaveChanges();
			}

			//Remove user from User table
			using(TempDBContext1 db = new TempDBContext1())
			{
				User user = db.Users.Where(u => u.UserName == userName).FirstOrDefault();
				if (user != null)
				{
					//db.Users.Attach(user);
					db.Users.Remove(user);
					db.Entry(user).State = EntityState.Deleted;
					db.SaveChanges();
				}
			}

			//Remove user from LogUser table and UserRoles table
			using(TempDBLogUsers db = new TempDBLogUsers())
			{
				LogUser user = db.LogUsers.Where(u => u.UserName == userName).FirstOrDefault();
				if (user != null)
				{
					user.Roles.FirstOrDefault().LogUsers.Remove(user);
					user.Roles.Remove(user.Roles.FirstOrDefault());
					//db.LogUsers.Attach(user);
					db.LogUsers.Remove(user);
					db.Entry(user).State = EntityState.Deleted;
					db.SaveChanges();
				}
			}
		}

		public IEnumerable<LogUser> GetAllLogUsers()
		{
			IEnumerable<LogUser> users = new List<LogUser>();
			using(TempDBLogUsers db = new TempDBLogUsers())
			{
				users = db.LogUsers.ToList();
			}
			return users;
		}

        public IEnumerable<Models.User> GetAllUsers()
        {
            IEnumerable<User> users = null;
            using (TempDBContext1 db = new TempDBContext1())
            {
                users = db.Users.ToList();
            }
            return users;
        }

        public IEnumerable<string> GetAllUsernames()
        {
            IEnumerable<User> users = null;
            List<string> names = new List<string>();
            using (TempDBContext1 db = new TempDBContext1())
            {
                users = db.Users.ToList();
            }
            if(users != null && users.Count() > 0)
            {
                foreach (User u in users)
                {
                    names.Add(u.UserName);
                }
            }
            return names;
        }

        public void CreateMessage(Message message)
        {
            using(TempDBFriendContext db = new TempDBFriendContext())
            {
                db.Messages.Add(message);
                db.SaveChanges();
            }
        }

        public void RemoveMessage(int id)
        {
            using (TempDBFriendContext db = new TempDBFriendContext())
            {
                try
                {
                    Message message = db.Messages.Where(x => x.Id == id).ToList().First();
                    db.Messages.Remove(message);
                    db.SaveChanges();
                }
                catch(Exception)
                {

                }
            }
        }

        public IEnumerable<Message> GetMessagesFor(User user)
        {
            IEnumerable<Message> messages = null;
            using (TempDBFriendContext db = new TempDBFriendContext())
            {
                messages = db.Messages.Where(x => x.Receiver.Equals(user.UserName)).ToList();
            }
            return messages;
        }

        public void CreateFriendRequest(FriendRequest fr)
        {
            using (TempDBFriendContext db = new TempDBFriendContext())
            {
                db.FriendRequests.Add(fr);
                db.SaveChanges();
            }
        }

        public void RemoveFriendRequest(FriendRequest fr)
        {
            using (TempDBFriendContext db = new TempDBFriendContext())
            {
                FriendRequest request = db.FriendRequests.Where(x => x.Receiver == fr.Receiver && x.Sender == fr.Sender).First();
                db.FriendRequests.Remove(request);
                db.SaveChanges();
            }
        }

        public IEnumerable<FriendRequest> GetFriendRequests(User user)
        {
            IEnumerable<FriendRequest> requests = null;
            using (TempDBFriendContext db = new TempDBFriendContext())
            {
                requests = db.FriendRequests.Where(x => x.Receiver.Equals(user.UserName)).ToList();
            }
            return requests;
        }

        public void AddFriend(string user, string newFriend)
        {
            using (SqlDBFriendListContext db = new SqlDBFriendListContext())
            {
                db.Friends.Add(new Friend() { User = user, Friend1 = newFriend });
                db.SaveChanges();
            }
        }

        public void RemoveFriend(string user, string friend)
        {
            using (SqlDBFriendListContext db = new SqlDBFriendListContext())
            {
                Friend toRemove;
                try
                {
                    toRemove = db.Friends.Where(x => x.User == user && x.Friend1 == friend).First();
                }
                catch(Exception)
                {
                    toRemove = db.Friends.Where(x => x.User == friend && x.Friend1 == user).First();
                }
                db.Friends.Remove(toRemove);
                db.SaveChanges();
            }
        }

        public IEnumerable<string> GetFriends(User user)
        {
            IEnumerable<string> friends = null;
            using (SqlDBFriendListContext db = new SqlDBFriendListContext())
            {
                IEnumerable<string> temp = db.Friends.Where(x => x.User.Equals(user.UserName)).Select(x => x.Friend1).ToList();
                IEnumerable<string> temp2 = db.Friends.Where(x => x.Friend1.Equals(user.UserName)).Select(x => x.User).ToList();
                friends = temp.Union(temp2).ToList();
            }
            return friends;
        }
    }
}