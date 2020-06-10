using MyTestChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTestChat.Interfaces
{
    public interface IChatMessage
    {
        void Create(ChatDialogs chatDialogs);
        Task<ChatDialogs> GetChatDialogs(int id);
        Task<IEnumerable<ChatDialogs>> GetChatAll();
        void Edit(ChatDialogs chatDialogs);
    }
}
