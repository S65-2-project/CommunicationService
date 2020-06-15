using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunicationService.Domain;
using CommunicationService.Exceptions;
using CommunicationService.Hubs;
using CommunicationService.Models;
using CommunicationService.Repository;

namespace CommunicationService.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _repository;
        private readonly ILiveChatService _liveChatService;

        public ChatService(IChatRepository repository, ILiveChatService liveChatService)
        {
            _repository = repository;
            _liveChatService = liveChatService;
        }

        public async Task<Chat> InitializeChat(InitializeModel initModel)
        {
            var newChat = new Chat()
            {
                Buyer = initModel.Buyer,
                Seller = initModel.Seller,
                Messages = new List<Message>()
            };
            var chatExists = await _repository.FindChat(newChat.Buyer, newChat.Seller);
            if (chatExists != null) 
            {
                throw new ChatAlreadyExistsException(chatExists.Id);
            }
            
            return await _repository.Create(newChat);
        }

        public async Task<Chat> SendMessage(Guid id, MessageModel message)
        {
            var newMessage = new Message()
            {
                Id = Guid.NewGuid(),
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
            var result = await _repository.Update(id, chatWithNewMessage);
            
            var liveMessage = new LiveMessageModel()
            {
                ChatId = id,
                Message = newMessage,
            };
            _liveChatService.SendPrivateMessage(liveMessage, 
                chatWithNewMessage.Buyer.Id != message.SenderId ? 
                    chatWithNewMessage.Buyer.Id : chatWithNewMessage.Seller.Id);

            return result;
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