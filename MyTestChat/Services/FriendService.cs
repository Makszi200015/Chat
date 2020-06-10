using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyTestChat.Models;

namespace MyTestChat.Services
{
    public class FriendService : IFriend
    {
        private ApplicationContext db;

        public FriendService(ApplicationContext db) 
        {
            this.db = db;
        }
        public void Create(Friends friends)
        {
            db.Friends.Add(friends);
            db.SaveChanges();
        }

        public void Edit(Friends friends)
        {
            db.Friends.Update(friends);
            db.SaveChanges();
        }

        public async Task<Friends> GetFriend(string id)
        {
            Friends friends = await db.Friends.FirstOrDefaultAsync(p => p.id == id);
            return friends;
        }

        public async Task<IEnumerable<Friends>> GetFriendsAll()
        {
            IEnumerable<Friends> friends = await db.Friends.ToListAsync();
            return friends;  
        }
    }
}
