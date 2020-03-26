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
            UserRepository userRepository = new UserRepository();
            CitataRepository citataRepository = new CitataRepository();

            User user1 = new User("login1", "password1", new int[] { 1, 2 });
            Citata citata1 = new Citata(citataRepository.LastId, "Title1", "Content1", "26.03.2020");
            Citata citata2 = new Citata(citataRepository.LastId, "Title2", "Content2", "25.03.2020");

            userRepository.AddUser(user1);
            var temp = String.Join(" ", user1.CitataIds);
            citataRepository.AddCitata(citata1);
            citataRepository.AddCitata(citata2);

            User user11 = (User)userRepository.GetUser("login1");
            Citata citata11 = (Citata)citataRepository.GetCitata(1);
            Citata citata21 = (Citata)citataRepository.GetCitata(2);

            Console.WriteLine(user11.Login + " " + user11.Password + " " + user11.CitataIds.ToString());
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
