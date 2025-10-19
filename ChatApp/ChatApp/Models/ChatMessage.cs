using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class ChatMessage
    {
        public string Type { get; set; } = "text"; // "text", "ping", "logout"
        public string From { get; set; }
        public string To { get; set; } = "ALL";
        public string Text { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
    }
}
