using DotnetChat.Core.Models;
using System.Threading.Tasks;

namespace DotnetChat.Core.Interfaces
{
    public interface IHubProvider
    {
        Task NotifyAll(MessageRequest message);
    }
}
