using DotnetChat.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetChat.Core.Interfaces
{
    public interface IChatService
    {
        Task<MessageResponse> SaveMessage(MessageRequest message);
        Task<List<MessageResponse>> GetMessages(Guid userId);
    }
}
