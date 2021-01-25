using DotnetChat.Core.Interfaces;
using DotnetChat.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotnetChat.Service
{

    public class ChatService : IChatService
    {
        public Task<MessageResponse> SaveMessage(MessageRequest message)
        {
            throw new NotImplementedException();
        }
    }
}
