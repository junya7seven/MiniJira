using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Interfaces
{
    public interface ITaskItemRepository
    {
        Task<IEnumerable<TaskItem>> GetAllTasksAsync();
        Task<TaskItem> GetTaskByIdAsync(int taskId);
        Task<bool> AddTaskAsync(TaskItem task);
        Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(int projectId);
        Task<bool> UpdateTaskAsync(TaskItem task);
        Task DeleteTaskAsync(string name);
    }
}
