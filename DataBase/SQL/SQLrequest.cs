using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.SQL
{
    public static class SQLrequest
    {
        public static string InsertUserSQL()
        {
            var SQL = @"INSERT INTO user_info(Name, Password, Email)
                        VALUES(@Name,@Password,@Email)
                        ON CONFLICT(Name, Email)
                        DO NOTHING;";
            return SQL;
        }
        public static string GetUserByNameSQL()
        {
            var SQL = @"SELECT *
                        FROM user_info
                        WHERE Email = @Email;";
            return SQL;
        }
    }
}
