using DataBase.Interfaces;
using DataBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniJiraWeb.Models;
using MiniJiraWeb.Service.DbService;
using System.Security.Claims;
using System.Threading.Tasks;

[Authorize]
public class ProjectController : Controller
{
    private readonly MiniJiraService _miniJiraService;

    public ProjectController(MiniJiraService miniJiraService)
    {
        _miniJiraService = miniJiraService;
    }

    public async Task<IActionResult> Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            var projects = await _miniJiraService.GetAllProjectsAsync();
            return View(projects);
        }
        else
        {
            TempData["ErrorMessage"] = "Вам нужно войти в систему, чтобы увидеть проекты.";
            return RedirectToAction("Login", "Account");
        }
    }


    public async Task<IActionResult> Project(int projectId)
    {
        var project = await _miniJiraService.GetProjectByIdAsync(projectId);
        var tasks = await _miniJiraService.GetTasksByProjectIdAsync(projectId);

        var viewModel = new ProjectViewModel
        {
            Projects = new List<Project> { project },
            Tasks = tasks
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateTaskStatus([FromBody] TaskStatusUpdateModel model)
    {
        var task = await _miniJiraService.UpdateTaskAsync(model.TaskTitle, model.NewStatus);

        return Ok();
    }


    [HttpPost]
    public async Task<IActionResult> CreateProject(Project project)
    {
        if (ModelState.IsValid)
        {
            var userId = User.FindFirstValue("UserId");
            project.CreatedByUserId = int.Parse(userId);
            if(await _miniJiraService.AddProjectAsync(project, userId));
                return RedirectToAction("Index");
        }
        return View(project);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(TaskItem task)
    {
        if (ModelState.IsValid)
        {
            var userId = User.FindFirstValue("UserId");
            if(await _miniJiraService.AddTaskAsync(task, userId)) 
                return RedirectToAction("Project", new { projectId = task.ProjectId });
        }
        return View(task);
    }

    public async Task<IActionResult> GetTaskDetails(int taskId)
    {
        var task = await _miniJiraService.GetTaskByIdAsync(taskId);
        if (task == null)
        {
            return NotFound();
        }
        return Json(new { title = task.Title, description = task.Description });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteTask([FromBody] string taskName)
    {
        await _miniJiraService.DeleteTaskAsync(taskName);
        return Ok();
    }
}
