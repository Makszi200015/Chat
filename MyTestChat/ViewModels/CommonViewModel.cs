using Microsoft.AspNetCore.Identity;
using MyTestChat.Interfaces;
using MyTestChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MyTestChat.ViewModels.UserViewModel;

namespace MyTestChat.ViewModels
{
    public class CommonViewModel
    {
        IFriend friend;
        IChatMessage chatMessage;
        private readonly UserManager<User> user;
        string SelfId { get; set; }
        public CommonViewModel(IFriend fr, IChatMessage chatMessages, UserManager<User> us, string selfid) 
        {
            friend = fr;
            chatMessage = chatMessages;
            user = us;
            SelfId = selfid;
            friends = new List<Friends>();
            chatMessagess = new List<List<ChatDialogs>>();
            sortusers = new List<UserViewModel>();
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
            var users = user.Users.ToList();
            var currentusers = user.FindByIdAsync(SelfId);
            currentusers.Wait();
            foreach(var item in users) 
            {
                var tempuser = new UserViewModel { Id = item.Id, Username = item.UserName};
                if (item.Id != currentusers.Result.Id) 
                {
                    if(friends.Count() != 0) 
                    {
                        if(friends.FirstOrDefault(x => x.Username1 == item.UserName && x.Username2 == currentusers.Result.UserName && x.Status == 0) != null) 
                        {
                            tempuser.Status = (int)UserStatus.Subscriber;
                        }
                        else if (friends.FirstOrDefault(x => x.Username2 == item.UserName && x.Username1 == currentusers.Result.UserName && x.Status == 0) != null) 
                        {
                            tempuser.Status = (int)UserStatus.ToSubscribe;
                        }
                        else if (friends.FirstOrDefault(x => x.Username1 == item.UserName && x.Username2 == currentusers.Result.UserName && x.Status == 1) != null && friends.FirstOrDefault(x => x.Username2 == item.UserName && x.Username1 == currentusers.Result.UserName && x.Status == 1) != null) 
                        {
                            tempuser.Status = (int)UserStatus.Friend;
                        }
                        else 
                        {
                            tempuser.Status = (int)UserStatus.Free;
                        }
                        sortusers.Add(tempuser);
                    }
                }
            }
           
            
        }
        public IEnumerable<Friends> friends;
        public List<List<ChatDialogs>> chatMessagess;
        public List<UserViewModel> sortusers;
    }
}
