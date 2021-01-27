using DotnetChat.Core.Interfaces;
using DotnetChat.Core.Models;
using DotnetChat.Infrasctructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DotnetChat.Infrasctructure
{
    public class HubProvider : IHubProvider
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly ILogger _logger;

        public HubProvider(IHubContext<ChatHub> hubContext, ILogger<HubProvider> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }
        
        public async Task NotifyAll(MessageRequest message)
        {
            _logger.LogInformation("sending back notification", message);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message.Email, message.Text, message.UserId);
        }
    }
}
