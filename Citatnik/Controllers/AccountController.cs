using Citatnik.DataBase;
using Citatnik.Models;
using Citatnik.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Citatnik.Controllers
{
    public class AccountController : Controller
    {
        private UserRepository db;
        private IHttpContextAccessor accessor;
        private ILogger<Startup> logger;
        public AccountController(UserRepository context, IHttpContextAccessor accessor, ILogger<Startup> logger)
        {
            db = context;
            this.accessor = accessor;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());
            var ip = heserver.AddressList[2].ToString();
            var tempIp = accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();

            if (ModelState.IsValid)
            {
                User user = db.Select(model.Login); //db.Users.FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
                if (user != null)
                {
                    if(user.Password != model.Password)
                    {
                        logger.LogInformation("Попытка входа с не корректным паролем. IP адресс: " + ip);
                        ModelState.AddModelError("", "Некорректный пароль");
                        return View(model);
                    }
                       
                    await Authenticate(model.Password); // аутентификация

                    logger.LogInformation("Вошёл пользователь с IP адрессом: " + ip + " и логином: " + model.Login);

                    return RedirectToAction("Index", "Home");
                }
                logger.LogInformation("Попытка входа с не корректным логином. IP адресс: " + ip);
                ModelState.AddModelError("", "Некорректный логин");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());
            var ip = heserver.AddressList[2].ToString();
            //var ip = accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();

            if (ModelState.IsValid)
            {
                User user = db.Select(model.Login);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    db.Insert(new User(model.Login, model.Password, new List<int>()));

                    await Authenticate(model.Login); // аутентификация

                    logger.LogInformation("Зарегестрирован пользователь с IP адрессом: " + ip + " и логином: " + model.Login);

                    return RedirectToAction("Index", "Home");
                }
                else {
                    logger.LogInformation("Попытка регистрации пользователя с уже существующем логином. IP адресс: " + ip);
                    ModelState.AddModelError("", "Такой пользователь уже существует");
                }
            }
            return View(model);
        }

        private async Task Authenticate(string userLogin)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userLogin)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
