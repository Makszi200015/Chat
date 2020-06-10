using MyTestChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTestChat
{
    public interface IFriend
    {
        void Create(Friends friends);
        Task<Friends> GetFriend(string id);
        Task<IEnumerable<Friends>> GetFriendsAll();
        void Edit(Friends friends);
    }
}
