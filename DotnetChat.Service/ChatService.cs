using AutoMapper;
using DotnetChat.Core.Interfaces;
using DotnetChat.Core.Interfaces.Repositories;
using DotnetChat.Core.Models;
using DotnetChat.Data.Entities;
using DotnetChat.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetChat.Service
{
    public class ChatService : IChatService
    {
        private readonly IRepository<Message> _messageRepo;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ChatService(IRepository<Message> repository, IMapper mapper, ILogger<ChatService> logger)
        {
            _messageRepo  = repository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<MessageResponse> SaveMessage(MessageRequest message)
        {
            var messageEntity = _mapper.Map<Message>(message);
            messageEntity.CreatedAt = DateTime.Now;
            _logger.LogInformation("saving message recevied", messageEntity);
            return _mapper.Map<MessageResponse>(await _messageRepo.Add(messageEntity));
        }
        public async Task<List<MessageResponse>> GetMessages(Guid userId)
        {
            _logger.LogInformation("getting messages for userId", userId);
            var meessages = await _messageRepo.Get(m => m.UserId == userId);
            var meessagesL = meessages.ToList();
            var list = _mapper.Map<List<MessageResponse>>(meessagesL);
            return list;
        }
    }
}
