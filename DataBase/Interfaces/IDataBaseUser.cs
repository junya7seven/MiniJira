using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Interfaces
{
    public interface IDataBaseUser
    {
        Task<bool> InsertUserAsync(User model);
        Task<User> GetVerifyUserAsync(User model);

    }
}
