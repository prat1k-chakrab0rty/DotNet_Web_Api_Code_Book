using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_Web_Api_Code_Book.Repo
{
    public class Utility
    {
        public static void AddDatabaseOutputParameters(SqlCommand cmd)
        {
            SqlParameter ErrorCode = new SqlParameter("@errorCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            SqlParameter ErrorMessage = new SqlParameter("@errorMsg", SqlDbType.NVarChar, 100) { Direction = ParameterDirection.Output };

            cmd.Parameters.Add(ErrorCode);
            cmd.Parameters.Add(ErrorMessage);
        }
    }
}
