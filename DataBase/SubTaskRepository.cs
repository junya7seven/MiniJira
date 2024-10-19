using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Models;
using Dapper;
using DataBase.Interfaces;
using System.Data;

namespace DataBase
{
    public class SubTaskRepository : ISubTaskRepository
    {
        private readonly IDbConnection _dbConnection;

        public SubTaskRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task AddSubTaskAsync(SubTask subTask)
        {
            var sql = "INSERT INTO SubTasks (Title, TaskItemId) VALUES (@Title, @TaskItemId)";


            await _dbConnection.ExecuteAsync(sql, subTask);

        }
    }

}
