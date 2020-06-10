using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyTestChat.Models
{
    public class ChatDialogs
    {
        public int Id { get; set; }                  
        public string UsName1 { get; set; }
        public string ChatName { get; set; }
        public string Message { get; set; }
    }
    
}
