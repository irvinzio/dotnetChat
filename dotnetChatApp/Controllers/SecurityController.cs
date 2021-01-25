using DotnetChat.Core.Interfaces;
using DotnetChat.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace dotnetChatApp.Controllers
{
    [Route("api/security")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        public SecurityController(ISecurityService securityService)
        {
            _securityService = securityService;
        } 
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> login([FromBody] LogInRequest loginRequest)
        {
            var user = await _securityService.LogIn(loginRequest);
            return Ok(user);
        }
        [Route("logout")]
        [HttpPost]
        public async Task<IActionResult> logout()
        {
            await _securityService.LogOut();
            return NoContent();
        }
    }
}
