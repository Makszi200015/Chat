using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyTestChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTestChat.Controllers
{
    public class FriendsController : Controller
    {
        UserManager<User> user;
        IFriend fr;
        ApplicationContext db;
        public FriendsController(IFriend f, UserManager<User> us, ApplicationContext _db)
        {
            user = us;
            fr = f;
            db = _db;
        }
        [HttpPost]
        public void Create([FromBody]Friends friends)
        {
            if (friends != null)
            {
                Friends friends1 = new Friends { id = friends.id, Username1 = friends.Username1, Username2 = friends.Username2, Status = friends.Status };
                string idi = friends1.Username2 + friends1.Username1;
                var l = fr.GetFriend(idi).Result;
                if (l != null)
                {
                    friends.Status = 1;
                    l.Status = 1;
                }
                else
                {
                    friends.Status = 0;
                }
                fr.Create(friends);
            }
        }
        [HttpGet]
        public void Delete() 
        {
            db.Friends.RemoveRange(db.Friends);
            db.SaveChanges();
        }
        public IActionResult Create()
        {
            return View(user.Users.ToList());
        }

    }
}