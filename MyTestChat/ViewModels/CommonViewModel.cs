using Microsoft.AspNetCore.Identity;
using MyTestChat.Interfaces;
using MyTestChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTestChat.ViewModels
{
    public class CommonViewModel
    {
        IFriend friend;
        IChatMessage chatMessage;
        private readonly UserManager<User> user;
        public CommonViewModel(IFriend fr, IChatMessage chatMessages, UserManager<User> us) 
        {
            friend = fr;
            chatMessage = chatMessages;
            user = us;
            friends = new List<Friends>();
            chatMessagess = new List<List<ChatDialogs>>();
            users = new List<List<User>>();
            GetFriends();
            GetChats();
            GetUsers();
        }
        private void GetFriends()
        {
            friends = friend.GetFriendsAll().Result;
        }
        private void GetChats()
        {
            var chat = chatMessage.GetChatAll().Result;
            var chatnames = chat.Select(u=>u.ChatName).Distinct();
            foreach(var item in chatnames) 
            {
                chatMessagess.Add(chat.Where(x=>x.ChatName == item).ToList());
            }
            
        }
        private void GetUsers()
        {

            var list = user.Users.ToList();
            if (friends.Count() != 0) 
            {
                foreach (var fr in friends)
                {
                    var nlist = list.Where(t => t.UserName != fr.Username1 && t.UserName != fr.Username2).Select(x => x.UserName).Distinct();

                    foreach (var us in nlist)
                    {
                        users.Add(list.Where(h => h.UserName == us).ToList());
                    }
                }
            }
            else
                foreach (var us in list)
                {
                    users.Add(list.Where(h => h.UserName == us.UserName).ToList());
                }

            users = users.Distinct().ToList();
            
        }
        public IEnumerable<Friends> friends;
        public List<List<ChatDialogs>> chatMessagess;
        public List<List<User>> users;
    }
}
