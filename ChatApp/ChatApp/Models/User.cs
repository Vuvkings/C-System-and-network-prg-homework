using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsOnline { get; set; } = false;
    }
}
