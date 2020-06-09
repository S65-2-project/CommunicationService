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

        public async Task<Chat> SendMessage(Guid id, MessageModel message)
        {
            var newMessage = new Message()
            {
                SenderId = message.SenderId,
                Text = message.Text,
                TimeStamp = DateTime.Now,
                Read = false
            };

            var chatWithNewMessage = await _repository.GetChat(id);
            if (chatWithNewMessage == null)
            {
                throw new ChatNotFoundException(id);
            }
            chatWithNewMessage.Messages.Add(newMessage);
            
            return await _repository.Update(id, chatWithNewMessage);
        }
    }
}