using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Citatnik.Models;
using Microsoft.AspNetCore.Authorization;
using Citatnik.ViewModels;
using Citatnik.DataBase;

namespace Citatnik.Controllers
{
    public class HomeController : Controller
    {
        private CitataRepository db;
        private ILogger<Startup> logger;

        public IActionResult Privacy()
        {
            return View();
        }
        public HomeController(CitataRepository context, ILogger<Startup> _logger)
        {
            db = context;
            logger = _logger;
        }

        [HttpGet]
        public IActionResult Index(HomeModel model)
        {
            model.Citatas.Add(db.Select(1));
            model.Citatas.Add(db.Select(36));
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCitataModel model)
        {
            Citata newCitata = new Citata(db.GetLastId(), model.Title, model.Title, DateTime.Now.ToString());
            db.Insert(newCitata);
            logger.LogInformation($"Создана цитата с id: {newCitata.CitataId} с заголовком: {newCitata.Title} с контентом: {newCitata.Content} и с датой: {newCitata.CreationDate}");
            return RedirectToAction("Index", "Home");
        }
    }
}
