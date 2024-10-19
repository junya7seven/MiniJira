using Dapper;
using DataBase.HashPasswordHelper;
using DataBase.Interfaces;
using DataBase.Models;
using DataBase.SQL;
using Npgsql;
using System.Data;

namespace DataBase
{
    public class DataBaseUser : IDataBaseUser
    {
        private readonly string _connectionString;
        private readonly IDbConnection _dbConnection;
        public DataBaseUser(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<bool> InsertUserAsync(User model)
        {
            model.Password = HashPassword.PasswordHash(model.Password);
            var SQL = SQLrequest.InsertUserSQL();
            try
            {
                return await _dbConnection.ExecuteAsync(SQL, model) > 0;
            }
            catch(Exception)
            {
                return false;
                throw;
            }
            
        }
        public async Task<User?> GetVerifyUserAsync(User model)
        {
            var SQL = SQLrequest.GetUserByNameSQL();
            try
            {
                
                 var user = await _dbConnection.QuerySingleOrDefaultAsync<User>(SQL, new { Email = model.Email });
                 if (HashPassword.VerifyPassword(model.Password, user.Password))
                 {
                    return user;
                 }
                 return null;
                
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
    }
}
