using AutoMapper;
using DotnetChat.Core.Interfaces;
using DotnetChat.Core.Interfaces.Repositories;
using DotnetChat.Core.Models;
using DotnetChat.Data.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetChat.Service.Extensions;
using DotnetChat.Infrasctructure;

namespace DotnetChat.Service
{
    public class ChatService : IChatService
    {
        private readonly IMessageRepository _messageRepo;
        private readonly IStockProvider _stockProvider;
        private readonly IHubProvider _hubProvider;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ChatService(IMessageRepository messageRepository, IMapper mapper, ILogger<ChatService> logger, IStockProvider stockProvider, IHubProvider hubProvider)
        {
            _messageRepo = messageRepository;
            _mapper = mapper;
            _logger = logger;
            _stockProvider = stockProvider;
            _hubProvider = hubProvider;
        }
        public async Task<MessageResponse> SaveMessage(MessageRequest message)
        {
            await _hubProvider.NotifyAll(message);
            if (message.Text.IsTextComand())
            {
                var stockInfo = _stockProvider.GetStocksInfo(message.Text);
                await _hubProvider.NotifyAll(new MessageRequest() { Text = stockInfo, UserId = new Guid(), Email = "bot@bot.com" });
                return null;
            }
            var messageEntity = _mapper.Map<Message>(message);
            messageEntity.CreatedAt = DateTime.Now;
            _logger.LogInformation("saving message recevied", messageEntity);
            return _mapper.Map<MessageResponse>(await _messageRepo.Add(messageEntity));
        }

        public async Task<List<MessageResponse>> GetMessages()
        {
            _logger.LogInformation("getting messages for userId");
            var meessages = await _messageRepo.GetAllMessages();
            var list = _mapper.Map<List<MessageResponse>>(meessages.OrderBy(m => m.CreatedAt));
            return list;
        }
    }
}
