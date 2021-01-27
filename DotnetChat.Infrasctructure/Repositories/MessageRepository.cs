using DotnetChat.Core.Interfaces.Repositories;
using DotnetChat.Data.Context;
using DotnetChat.Data.Entities;
using DotnetChat.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetChat.Infrasctructure.Repositories
{

    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private DotnetChatContext _context;
        public MessageRepository(DotnetChatContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Message>> GetAllMessages()
        {
            return await _context.Messages.AsQueryable()
                .Include(u => u.User)
                .ToListAsync();
        }
    }
}
