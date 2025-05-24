using DotNet_Web_Api_Code_Book.Common.DTOs;
using DotNet_Web_Api_Code_Book.Common.Models;

namespace DotNet_Web_Api_Code_Book.Service.Interface
{
    public interface IAuthService
    {
        public Task<Response> SignUp(User user);
        public Task<Response> SignIn(string username, string password);
        public Task<Response> ChangePassword(string username, string oldPassword, string newPassword);
    }
}
