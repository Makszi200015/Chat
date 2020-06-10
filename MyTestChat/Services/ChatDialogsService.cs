using Microsoft.EntityFrameworkCore;
using MyTestChat.Interfaces;
using MyTestChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTestChat.Services
{
    public class ChatDialogsService : IChatMessage
    {
        private ApplicationContext db;

        public ChatDialogsService(ApplicationContext db)
        {
            this.db = db;
        }
        public void Create(ChatDialogs chat)
        {
            db.ChatDialogs.Add(chat);
            db.SaveChanges();
        }

        public void Edit(ChatDialogs chat)
        {
            db.ChatDialogs.Update(chat);
            db.SaveChanges();
        }

        public async Task<ChatDialogs> GetChatDialogs(int id)
        {
            ChatDialogs chat = await db.ChatDialogs.FirstOrDefaultAsync(p => p.Id == id);
            return chat;
        }

        public async Task<IEnumerable<ChatDialogs>> GetChatAll()
        {
            IEnumerable<ChatDialogs> chats = await db.ChatDialogs.ToListAsync();
            return chats;
        }
    }
}
