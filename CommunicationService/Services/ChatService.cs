using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunicationService.Domain;
using CommunicationService.Exceptions;
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

        public async Task<List<Chat>> GetUserChats(Guid id)
        {
            var list = await _repository.GetUserChats(id);
            if (list == null)
            {
                throw new NotFoundException("There does not exists a user with this id");
            }

            return list;
        }

        public async Task<List<Message>> GetChatMessages(Guid id)
        {
            var list = await _repository.GetChatMessages(id);
            if (list == null)
            {
                throw new NotFoundException("There does not exists a chat with this id");
            }

            return list;
        }

        public async Task ReadChat(Guid id, Guid userId)
        {
            await _repository.ReadChat(id, userId);
        }
    }
}