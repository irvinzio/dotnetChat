using System.Threading.Tasks;
using DotnetChat.Core.Models;
using Microsoft.AspNetCore.SignalR;

namespace DotnetChat.Infrasctructure.Hubs
{
    public class ChatHub : Hub
    {
        public Task SendMessage(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveOne", user, message);
        }
    }
}