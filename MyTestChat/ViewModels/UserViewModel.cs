using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTestChat.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public int Status { get; set; }
       public enum UserStatus
        {
            Free = 1,
            Subscriber,
            ToSubscribe,
            Friend
        }
    }
}
