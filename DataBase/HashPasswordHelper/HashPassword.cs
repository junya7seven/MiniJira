using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.HashPasswordHelper
{
    public static class HashPassword
    {
        public static string PasswordHash(string password)
        {
            string hash = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);
            return hash;
        }
        public static bool VerifyPassword(string  password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
        }
    }
}
