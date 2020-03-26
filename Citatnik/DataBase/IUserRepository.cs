using Citatnik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Citatnik.DataBase
{
    interface IUserRepository
    {
        public void AddUser(User user);
        public User GetUser(string login);
    }
}
