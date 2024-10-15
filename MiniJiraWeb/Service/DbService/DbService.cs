using DataBase.Interfaces;
using DataBase.Models;
using MiniJiraWeb.Models;

namespace MiniJiraWeb.Service.DbService
{
    public class DbService
    {
        private readonly IDataBaseUser _dbUser;
        public DbService(IDataBaseUser dbUser)
        {
            _dbUser = dbUser;
        }

        public async Task<bool> RegisterUser(RegisterUser model)
        {
            var user = new User()
            {
                Name = model.Name,
                Password = model.Password,
                Email = model.Email
            };
            if(await _dbUser.InsertUserAsync(user))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> LoginUser(LoginUser model)
        {
            var user = new User()
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
            };
            if(await _dbUser.GetVerifyUserAsync(user) != null)
            {
                return true;
            }
            return false;
        }
    }
}
