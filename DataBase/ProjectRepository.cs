using Dapper;
using DataBase.Interfaces;
using DataBase.Models;
using DataBase.SQL;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IDbConnection _dbConnection;

        public ProjectRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            var sql = "SELECT * FROM Projects";
            return await _dbConnection.QueryAsync<Project>(sql);
        }

        public async Task<Project> GetProjectByIdAsync(int projectId)
        {
            var sql = "SELECT * FROM Projects WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Project>(sql, new { Id = projectId });

        }

        public async Task<bool> AddProjectAsync(Project project)
        {
            try
            {
                var sql = "INSERT INTO Projects (Name, CreatedByUserId) VALUES (@Name, @CreatedByUserId)";
                return await _dbConnection.ExecuteAsync(sql, project) > 0;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
