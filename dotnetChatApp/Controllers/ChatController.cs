using DotnetChat.Core.Interfaces;
using DotnetChat.Core.Models;
using DotnetChat.Infrasctructure.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetChatApp.Controllers
{
    [Authorize]
    [Route("api/chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly ILogger _logger;

        public ChatController(IChatService chatService, ILogger<ChatController> logger)
        {
            _chatService = chatService;
            _logger = logger;
        }

        [Route("send")]
        [HttpPost]
        public async Task<IActionResult> SendRequest([FromBody] MessageRequest message)
        {
            _logger.LogInformation("message recevied", message);
            await _chatService.SaveMessage(message);
            return Ok();
        }
        [Route("retrieve")]
        [HttpGet]
        public async Task<ActionResult<List<MessageResponse>>> RetrieveMessage()
        {
            var messages = await _chatService.GetMessages();
            return Ok(messages);
        }
    }
}