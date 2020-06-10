using Microsoft.AspNetCore.Mvc;
using MyTestChat.Interfaces;
using MyTestChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTestChat.Controllers
{
    public class ChatController: Controller
    {
        IChatMessage chatMessage;
        public ChatController(IChatMessage ch)
        {
            chatMessage = ch;
        }
        [HttpPost]
        public void Create([FromBody]ChatDialogs chatDialogs)
        {
            if (chatDialogs != null)
            {
                chatMessage.Create(chatDialogs);
            }
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
