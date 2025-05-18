using DotNet_Web_Api_Code_Book.Common.DTOs;
using DotNet_Web_Api_Code_Book.Common.Models;
using DotNet_Web_Api_Code_Book.Repo.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DotNet_Web_Api_Code_Book.Repo
{
    public class AuthRepo : IAuthRepo
    {
        private readonly string _connectionString;

        public AuthRepo(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        public async Task<User?> ValidateCredentials(string username, string password)
        {
            //ADO.NET (running SPs)
            using var connection = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("Auth.ValidateCredentials", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            SqlParameter ErrorCode = new SqlParameter("@errorCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            SqlParameter ErrorMessage = new SqlParameter("@errorMsg", SqlDbType.NVarChar, 100) { Direction = ParameterDirection.Output };

            cmd.Parameters.Add(ErrorCode);
            cmd.Parameters.Add(ErrorMessage);

            await connection.OpenAsync();

            try
            {
                User? user = null;

                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    user = new User
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = reader["FirstName"].ToString()!,
                        LastName = reader["LastName"].ToString()!,
                        UserName = reader["UserName"].ToString()!,
                        Password = reader["Password"].ToString()!,
                        City = reader["City"].ToString()!,
                    };
                }

                await reader.CloseAsync();
                return user;
            }
            catch (SqlException ex)
            {
                return null;
            }
        }
        public async Task<Response> SignUp(User user)
        {
            //ADO.NET (running SPs)
            using var connection = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("Auth.SignUp", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fName", user.FirstName);
            cmd.Parameters.AddWithValue("@lName", user.LastName);
            cmd.Parameters.AddWithValue("@username", user.UserName);
            cmd.Parameters.AddWithValue("@username", user.UserName);
            cmd.Parameters.AddWithValue("@password", user.Password);
            cmd.Parameters.AddWithValue("@city", user.City);
            connection.Open();
            try
            {
                await cmd.ExecuteNonQueryAsync();
                return new Response
                {
                    StatusCode = 200,
                    StatusMessage = "OK",
                    Payload = "User created successfully"
                };
            }
            catch (SqlException ex)
            {
                return new Response
                {
                    StatusCode = 500,
                    StatusMessage = "Internal Server Error",
                    Payload = ex.Message
                };
            }
        }
    }
}
