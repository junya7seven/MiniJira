using Dapper;
using DataBase.Interfaces;
using DataBase.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly IDbConnection _dbConnection;

        public TaskItemRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            var sql = "SELECT * FROM Tasks";
            return await _dbConnection.QueryAsync<TaskItem>(sql);

        }

        public async Task<TaskItem> GetTaskByIdAsync(int taskId)
        {
            var sql = "SELECT * FROM Tasks WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<TaskItem>(sql, new { Id = taskId });

        }

        public async Task<bool> AddTaskAsync(TaskItem task)
        {
            try
            {
                var sql = @"INSERT INTO Tasks (Title, Description, Status, ProjectId, CreatedByUserId, Description_Date) 
                            VALUES (@Title, @Description, @Status, @ProjectId, @CreatedByUserId, @Description_Date)";
                return await _dbConnection.ExecuteAsync(sql, task) > 0;
            }
            catch(Exception)
            {
                return false;
                throw;
            }
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(int projectId)
        {
            var sql = "SELECT * FROM Tasks WHERE ProjectId = @ProjectId";
            return await _dbConnection.QueryAsync<TaskItem>(sql, new { ProjectId = projectId });
        }

        // Обновление задачи
        public async Task<bool> UpdateTaskAsync(TaskItem task)
        {
            var sql = "UPDATE Tasks SET Status = @Status, Last_ChangeDate = @Last_ChangeDate WHERE Title = @Title";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                task.Title,
                task.Status,
                task.Last_ChangeDate
            }) > 0;

        }

        public async Task DeleteTaskAsync(string title)
        {
            var sql = "DELETE FROM tasks WHERE title = @title";
            await _dbConnection.ExecuteAsync(sql, new {title = title });
        }
    }

}
