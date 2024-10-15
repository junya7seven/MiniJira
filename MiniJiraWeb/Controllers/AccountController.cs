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
            if (model.Email != null || model.Password != null)
            {
                if(await _db.LoginUser(model))
                {
                    var token = await _jwtService.GenerateToken(model);
                    Response.Cookies.Append("access_token", token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true, 
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTimeOffset.UtcNow.AddMinutes(60)
                    });
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Неверный логин или пароль.");
            }
            return View(model);

        }
    }
}
