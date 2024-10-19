using DataBase.Models;

namespace MiniJiraWeb.Models
{
    public class ProjectViewModel
    {
        public IEnumerable<Project> Projects { get; set; } = new List<Project>();
        public IEnumerable<TaskItem> Tasks { get; set; } = new List<TaskItem>();

    }
}
