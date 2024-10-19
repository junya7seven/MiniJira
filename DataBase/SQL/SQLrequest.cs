using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.SQL
{
    public static class SQLrequest
    {
        /// <summary>
        /// USER
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// PROJECT
        /// </summary>
        /// <returns></returns>
        public static string GetAllProjectsSQL()
        {
            var SQL = @"SELECT *
                        FROM Project;";
            return SQL;
        }
        
        public static string GetProjectByIdSQL()
        {
            var SQL = @"SELECT *
                        FROM Project
                        WHERE Project_Id = @Project_Id;";
            return SQL;
        }
        public static string AddProjectSQL()
        {
            var SQL = @"INSERT INTO Project(Project_Name,user_id)
                        VALUES(@Project_Name,@user_id);";
            return SQL;
        }
        public static string DeleteProjectSQL()
        {
            var SQL = @"DELETE FROM Project
                        WHERE Project_Id = @Project_Id;";
            return SQL;
        }
    }
}
