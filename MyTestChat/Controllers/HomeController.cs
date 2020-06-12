using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyTestChat.Interfaces;
using MyTestChat.Models;
using MyTestChat.ViewModels;

namespace MyTestChat.Controllers
{
    public class HomeController : Controller
    {
        UserManager<User> user;
        IFriend friend;
        IChatMessage chatMessage;
        public HomeController( UserManager<User> us, IFriend fr, IChatMessage ch)
        {
            friend = fr;
            user = us;
            chatMessage = ch;
        }

        public IActionResult Index1()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult MyPage()
        {
            CommonViewModel model = new CommonViewModel(friend,chatMessage,user,user.GetUserId(User));
            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
