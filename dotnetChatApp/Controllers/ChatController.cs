using DotnetChat.Core.Models;
using DotnetChat.Service;
using dotnetChatApp.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace dotnetChatApp.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly ChatService _chatService;

        public ChatController(IHubContext<ChatHub> hubContext, ChatService chatService)
        {
            _hubContext = hubContext;
            _chatService = chatService;
        }

        [Route("send")]
        [HttpPost]
        public async Task<IActionResult> SendRequest([FromBody] MessageRequest message)
        {
            await _chatService.SaveMessage(message);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message.Alias, message.Text);
            return Ok();
        }
    }
}