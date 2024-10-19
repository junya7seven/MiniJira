using DataBase;
using DataBase.Interfaces;
using DataBase.Models;
using System.Reflection;
using System.Threading.Tasks;

namespace MiniJiraWeb.Service.DbService
{
    public class MiniJiraService
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ISubTaskRepository _subTaskRepository;

        public MiniJiraService(ITaskItemRepository taskItemRepository, IProjectRepository projectRepository, ISubTaskRepository subTaskRepository)
        {
            _taskItemRepository = taskItemRepository;
            _projectRepository = projectRepository;
            _subTaskRepository = subTaskRepository;
        }

        public async Task<bool> AddProjectAsync(Project model, string userId)
        {
            var projectModel = new Project()
            {
                Name = model.Name,
                CreatedByUserId = int.Parse(userId)
            };
            if (await _projectRepository.AddProjectAsync(model)) return true;
            return false;
        }

        public async Task<bool> AddTaskAsync(TaskItem model, string userId)
        {
            var taskModel = new TaskItem()
            {
                Title = model.Title,
                Description = model.Description,
                Status = model.Status,
                ProjectId = model.ProjectId,
                CreatedByUserId = int.Parse(userId),
                Description_Date = DateTime.Now
            };
            if (await _taskItemRepository.AddTaskAsync(taskModel)) return true;
            return false;
        }
        public async Task<bool> UpdateTaskAsync(string title, string newStatus)
        {
            var status = TaskItemStatus.ToDo;
            if (newStatus == "ToDo")
                status = TaskItemStatus.ToDo;
            else if (newStatus == "In Progress")
                status = TaskItemStatus.InProgress;
            else if (newStatus == "Done")
                status = TaskItemStatus.Done;
            var taskModel = new TaskItem()
            {
                Title = title,
                Status = status,
                Last_ChangeDate = DateTime.Now
            };
            if (await _taskItemRepository.UpdateTaskAsync(taskModel)) return true;
            return false;
        }

        public async Task<bool> DeleteTaskAsync(string taskName)
        {
            await _taskItemRepository.DeleteTaskAsync(taskName);
            return true;
        }

        public async Task<TaskItem> GetTaskByIdAsync(int taskId)
        {
            var task = await _taskItemRepository.GetTaskByIdAsync(taskId);
            if (task != null)
            {
                return task;
            }
            return null;
        }

        public async Task<Project> GetProjectByIdAsync(int projectId)
        {
            var projects = await _projectRepository.GetProjectByIdAsync(projectId);
            return projects;
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(int projectId)
        {
            var tasks = await _taskItemRepository.GetTasksByProjectIdAsync(projectId);
            return tasks;
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllProjectsAsync();
            return projects;
        }
    }
}
