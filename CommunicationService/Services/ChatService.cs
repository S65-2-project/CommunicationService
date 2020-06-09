using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunicationService.Domain;
using CommunicationService.Models;
using CommunicationService.Repository;

namespace CommunicationService.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _repository;

        public ChatService(IChatRepository repository)
        {
            _repository = repository;
        }

        public async Task<Chat> InitializeChat(InitializeModel initModel)
        {
            var newChat = new Chat()
            {
                Buyer = initModel.Buyer,
                Seller = initModel.Seller,
                Messages = new List<Message>()
            };
            
            return await _repository.Create(newChat);
        }
    }
}