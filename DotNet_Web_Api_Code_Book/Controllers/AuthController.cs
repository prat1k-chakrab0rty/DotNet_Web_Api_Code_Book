using DotNet_Web_Api_Code_Book.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok("Logout");
        }

        [HttpGet("Signup")]
        public async Task<IActionResult> Signup()
        {
            return Ok();
        }
    }
}
