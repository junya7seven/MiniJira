using Microsoft.AspNetCore.Mvc;
using MiniJiraWeb.Models;
using MiniJiraWeb.Service.DbService;
using MiniJiraWeb.Service.JwtService;

namespace MiniJiraWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly JwtService _jwtService;
        private readonly DbService _db;
        public AccountController(JwtService jwtService, DbService db)
        {
            _jwtService = jwtService;
            _db = db;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(RegisterUser model)
        {
            if (ModelState.IsValid)
            {
                if (await _db.RegisterUser(model))
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUser model)
        {
            if (!string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.Password))
            {
                var user = await _db.LoginUser(model);
                if (user != null)
                {
                    var token = await _jwtService.GenerateToken(user);
                    Response.Cookies.Append("access_token", token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true, 
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTimeOffset.UtcNow.AddMinutes(60)
                    });
                    return RedirectToAction("Index", "Project");
                }
                ModelState.AddModelError(string.Empty, "Неверный логин или пароль.");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Введите корректные Email и Пароль.");
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            // Удаляем токен, установив его срок действия в прошлое
            Response.Cookies.Append("access_token", string.Empty, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(-1)
            });

            // Перенаправляем на страницу входа
            return RedirectToAction("Login", "Account");
        }

    }
}
