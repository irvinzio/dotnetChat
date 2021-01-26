using DotnetChat.Core.Interfaces;
using DotnetChat.Core.Models;
using dotnetChatApp.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace dotnetChatApp.Controllers
{
    [Authorize]
    [Route("api/chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IChatService _chatService;
        private readonly ILogger _logger;

        public ChatController(IHubContext<ChatHub> hubContext, IChatService chatService, ILogger<ChatController> logger)
        {
            _hubContext = hubContext;
            _chatService = chatService;
            _logger = logger;
        }

        [Route("send")]
        [HttpPost]
        public async Task<IActionResult> SendRequest([FromBody] MessageRequest message)
        {
            _logger.LogInformation("message recevied", message);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message.Alias, message.Text);
            await _chatService.SaveMessage(message);
            return Ok();
        }
    }
}