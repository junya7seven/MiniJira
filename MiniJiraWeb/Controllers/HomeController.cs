using DataBase.Interfaces;
using DataBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniJiraWeb.Models;
using System.Diagnostics;

namespace MiniJiraWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskItemRepository _taskItemRepository;

        public HomeController(IProjectRepository projectRepository, ITaskItemRepository taskItemRepository)
        {
            _projectRepository = projectRepository;
            _taskItemRepository = taskItemRepository;
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {


            return View();
        }




    }
}
