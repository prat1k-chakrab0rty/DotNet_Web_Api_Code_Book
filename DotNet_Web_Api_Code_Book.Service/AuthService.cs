using DotNet_Web_Api_Code_Book.Common.DTOs;
using DotNet_Web_Api_Code_Book.Common.Models;
using DotNet_Web_Api_Code_Book.Repo.Interface;
using DotNet_Web_Api_Code_Book.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_Web_Api_Code_Book.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepo _authRepo;
        private readonly IJWTTokenService _jWTTokenService;

        public AuthService(IAuthRepo authRepo, IJWTTokenService jWTTokenService)
        {
            _authRepo = authRepo;
            _jWTTokenService = jWTTokenService;
        }
        public async Task<Response> SignIn(string username, string password)
        {
            User user = await _authRepo.ValidateCredentials(username, password);
            string accessToken = _jWTTokenService.GenerateToken(user);
            return new Response()
            {
                StatusCode = 200,
                StatusMessage = "OK",
                Payload = new
                {
                    accessToken = accessToken
                }
            };
        }

        public async Task<Response> SignUp(User user)
        {
            return await _authRepo.SignUp(user);
        }
    }
}
