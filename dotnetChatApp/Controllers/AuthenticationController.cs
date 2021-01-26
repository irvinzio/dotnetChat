using DotnetChat.Core.Interfaces;
using DotnetChat.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace dotnetChatApp.Controllers
{
    [Route("api/security")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAutheticationService _authService;
        public AuthenticationController(IAutheticationService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest model)
        {
            var user = await _authService.LogIn(model);
            if(user == null) return Unauthorized();
            return Ok(user); 
        }
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<RegisterResponse>> Register([FromBody] RegisterRequest model)
        {
            var registerResponse = await _authService.Register(model);
            if (!registerResponse.Success) return BadRequest(registerResponse);
            return Ok(registerResponse);
        }
    }
}
