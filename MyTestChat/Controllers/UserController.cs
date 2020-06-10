using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyTestChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTestChat.Controllers
{
    public class UserController:Controller
    {
        UserManager <User> user;
        public UserController(UserManager<User> us) 
        {
            user = us;
        }
        public IActionResult Index() 
        {
            return View(user.Users.ToList());
        } 
      
        [HttpPost]
        public async Task<IActionResult> Delete(string id) 
        {
            var users = await user.FindByIdAsync(id);
            if (users != null) 
            {
                IdentityResult result = await user.DeleteAsync(users);
            }
            return RedirectToAction("Index");
        }

    }
}
