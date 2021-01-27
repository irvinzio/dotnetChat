using DotnetChat.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetChat.Core.Interfaces.Repositories
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<IEnumerable<Message>> GetAllMessages();
    }
}
