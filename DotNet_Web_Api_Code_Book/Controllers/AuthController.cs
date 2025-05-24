using DotNet_Web_Api_Code_Book.Common.Models;
using DotNet_Web_Api_Code_Book.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace DotNet_Web_Api_Code_Book.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("Login")]
        [SwaggerOperation(Summary = "Authenticates you and returns jwt bearer token")]
        public async Task<IActionResult> Login(
            [FromQuery, SwaggerParameter("The user's username", Required = true)] string username,
            [FromQuery, SwaggerParameter("The user's password", Required = true)] string password)
        {
            var res = await _authService.SignIn(username, password);
            return Ok(res);
        }

        [HttpPost("Signup")]
        [SwaggerOperation(Summary = "Creates a new User")]
        public async Task<IActionResult> Signup(
            [FromQuery, SwaggerParameter("The user's first name", Required = true)] string firstName,
            [FromQuery, SwaggerParameter("The user's last name", Required = true)] string lastName,
            [FromQuery, SwaggerParameter("The user's username", Required = true)] string username,
            [FromQuery, SwaggerParameter("The user's password", Required = true)] string password,
            [FromQuery, SwaggerParameter("The user's city", Required = false)] string city)
        {
            var newUser = new User
            {
                Id = 0,
                FirstName = firstName,
                LastName = lastName,
                UserName = username,
                Password = password,
                City = city
            };
            var res = await _authService.SignUp(newUser);
            if (res.StatusCode == 201)
            {
                return CreatedAtAction("Signup", res);
            }
            else
            {
                return BadRequest(res);
            }
        }

        [HttpGet("ChangeMyPassword")]
        [SwaggerOperation(Summary = "Once authenticated you can change your password")]
        [Authorize]
        public async Task<IActionResult> ChangeMyPassword(
            [FromQuery, SwaggerParameter("The user's old password", Required = true)] string oldPassword,
            [FromQuery, SwaggerParameter("The user's old password", Required = true)] string newPassword)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized("User not authenticated");
            }
            else
            {
                var res = await _authService.ChangePassword(username, oldPassword, newPassword);
                if (res.StatusCode == 200)
                {
                    return Ok(res);
                }
                else
                {
                    return BadRequest(res);
                }
            }
        }
    }
}
