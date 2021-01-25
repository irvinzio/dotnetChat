using AutoMapper;
using DotnetChat.Core.Interfaces;
using DotnetChat.Core.Interfaces.Repositories;
using DotnetChat.Core.Models;
using DotnetChat.Data.Entities;
using DotnetChat.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotnetChat.Service
{
    public class ChatService : IChatService
    {
        private readonly IRepository<Message> _messageRepo;
        private readonly IMapper _mapper;
        public ChatService(IRepository<Message> repository, IMapper mapper)
        {
            _messageRepo  = repository;
            _mapper = mapper;
        }
        public async Task<MessageResponse> SaveMessage(MessageRequest message)
        {
            var messageEntity =  _mapper.Map<Message>(message);
            return _mapper.Map<MessageResponse>(await _messageRepo.Update(messageEntity));
        }
    }
}
