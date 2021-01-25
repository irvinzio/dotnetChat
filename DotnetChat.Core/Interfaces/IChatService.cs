using DotnetChat.Core.Models;
using System.Threading.Tasks;

namespace DotnetChat.Core.Interfaces
{
    public interface IChatService
    {
        Task<MessageResponse> SaveMessage(MessageRequest message);
    }
}
