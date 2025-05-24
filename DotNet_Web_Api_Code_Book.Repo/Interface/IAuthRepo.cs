using DotNet_Web_Api_Code_Book.Common.DTOs;
using DotNet_Web_Api_Code_Book.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_Web_Api_Code_Book.Repo.Interface
{
    public interface IAuthRepo
    {
        public Task<Response> SignUp(User user);
        public Task<User> ValidateCredentials(string username, string password);
        public Task<Response> ChangePassword(string userName,string oldPassword, string newPassword);
    }
}
