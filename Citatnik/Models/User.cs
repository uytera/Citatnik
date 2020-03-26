using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Citatnik.Models
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int[] CitataIds { get; set; }

        public User(string login, string password, int[] citataIds)
        {
            Login = login;
            Password = password;
            CitataIds = citataIds;
        }
    }
}
