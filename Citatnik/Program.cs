using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Citatnik.DataBase;
using Citatnik.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Citatnik
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IUserRepository userRepository = new UserRepository();

            User user1 = new User("login1", "password1");
            User user2 = new User("login2", "password2");

            userRepository.AddUser(user1);
            userRepository.AddUser(user2);

            User user11 = (User)userRepository.GetUser("login1");
            User user21 = (User)userRepository.GetUser("login2");

            Console.WriteLine(user11.Login + " " + user11.Password);
            Console.WriteLine(user21.Login + " " + user21.Password);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
