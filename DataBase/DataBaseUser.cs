using Dapper;
using DataBase.HashPasswordHelper;
using DataBase.Interfaces;
using DataBase.Models;
using DataBase.SQL;
using Npgsql;

namespace DataBase
{
    public class DataBaseUser : IDataBaseUser
    {
        private readonly string _connectionString;
        public DataBaseUser(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> InsertUserAsync(User model)
        {
            model.Password = HashPassword.PasswordHash(model.Password);
            var SQL = SQLrequest.InsertUserSQL();
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    return await connection.ExecuteAsync(SQL, model) > 0;
                }
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
                using(var connection = new NpgsqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var user = await connection.QuerySingleOrDefaultAsync<User>(SQL, new { Email = model.Email });
                    if (HashPassword.VerifyPassword(model.Password, user.Password))
                    {
                        return user;
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
    }
}
