using massey_effect_matthews_mages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace massey_effect_matthews_mages.DataAL
{
    public interface IDAL
    {
        void UpdateUser(User user);
        void CreateUser(User user);
        User GetUser(string userName);
		LogUser GetLogUser(string userName);
        IEnumerable<User> GetAllUsers();
        IEnumerable<string> GetAllUsernames();
		IEnumerable<LogUser> GetAllLogUsers();
		bool isInRole(string userName, string roleName);
		void DeleteUser(string userName);
        void AddFriend(string user, string newFriend);
        void RemoveFriend(string user, string friend);
        IEnumerable<string> GetFriends(User user);

        void CreateMessage(Message message);
        void RemoveMessage(int id);
        IEnumerable<Message> GetMessagesFor(User user);

        void CreateFriendRequest(FriendRequest fr);
        void RemoveFriendRequest(FriendRequest fr);
        IEnumerable<FriendRequest> GetFriendRequests(User user);
    }
}