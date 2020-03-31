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
            UserRepository userRepository = UserRepository.getInstance();
            CitataRepository citataRepository = CitataRepository.getInstance();

            User user1 = new User("login1", "password1", new List<int> { 1, 2 });
            User user2 = new User("login2", "password2", new List<int> { 2 });
            Citata citata1 = new Citata(citataRepository.GetLastId(), "Title1", "Content1", "26.03.2020");
            Citata citata2 = new Citata(citataRepository.GetLastId(), "Title2", "Content2", "25.03.2020");

            userRepository.Insert(user1);
            userRepository.Insert(user2);
            citataRepository.Insert(citata1);
            citataRepository.Insert(citata2);

            User user11 = (User)userRepository.Select("login1");
            User user21 = (User)userRepository.Select("login2");
            Citata citata11 = (Citata)citataRepository.Select(1);
            Citata citata21 = (Citata)citataRepository.Select(2);

            Console.WriteLine(user11.Login + " " + user11.Password + " " + user11.CitataIds.ToArray());
            Console.WriteLine(user21.Login + " " + user21.Password + " " + user21.CitataIds.ToArray());
            Console.WriteLine(citata11.CitataId + " " + citata11.Title + " " + citata11.Content + " " + citata11.CreationDate);
            Console.WriteLine(citata21.CitataId + " " + citata21.Title + " " + citata21.Content + " " + citata21.CreationDate);

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
