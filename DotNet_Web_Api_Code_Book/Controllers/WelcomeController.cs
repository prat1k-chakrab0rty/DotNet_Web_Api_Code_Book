using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace DotNet_Web_Api_Code_Book.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WelcomeController : ControllerBase
    {
        private readonly ILogger<WelcomeController> _logger;

        public WelcomeController(ILogger<WelcomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("HelloDotNet")]
        [SwaggerOperation(Summary = "Welcomes you if authenticated")]
        public IActionResult HelloDotNet()
        {
            // Name identifier maps to 'sub' of claims
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _logger.LogInformation($"Hi, {username}! Welcome to console.");
            return Ok($"Hi, {username}! Welcome to secure .NET Web API.");
        }
    }
}
