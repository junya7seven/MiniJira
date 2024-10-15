using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniJiraWeb.Models;
using System.Diagnostics;

namespace MiniJiraWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }

        
    }
}
