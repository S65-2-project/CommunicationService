using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunicationService.Domain;
using CommunicationService.Models;
using CommunicationService.Repository;
using CommunicationService.Services;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace CommunicationServiceTest.ServiceTests
{
    public class ChatServiceTest
    {
        private readonly IChatService _chatService;
        private readonly Mock<IChatRepository> _repository;

        public ChatServiceTest()
        {
            _repository = new Mock<IChatRepository>();

            _chatService = new ChatService(
                _repository.Object);
        }

        [Fact]
        public async Task InitializeChat_Success_ReturnsCreatedChatObject()
        {
            var buyerId = Guid.NewGuid();
            var sellerId = Guid.NewGuid();
            var initModel = new InitializeModel()
            {
                Buyer = new User {Name = "test@test.test", Id = buyerId},
                Seller = new User {Name = "my@email.be", Id = sellerId}
            };
            
            var chatModel = new Chat()
            {
                Buyer = new User {Name = "test@test.test", Id = buyerId},
                Seller = new User {Name = "my@email.be", Id = sellerId},
                Messages = new List<Message>()
            };
            var returnChat = new Chat()
            {
                Id = Guid.NewGuid(),
                Buyer = new User {Name = "test@test.test", Id = buyerId},
                Seller = new User {Name = "my@email.be", Id = sellerId},
                Messages = new List<Message>()
            };

            _repository.Setup(x => x.Create(It.IsAny<Chat>())).ReturnsAsync(returnChat);

            var result = await _chatService.InitializeChat(initModel);

            Assert.Equal(chatModel.Buyer.Name, result.Buyer.Name);
            Assert.Equal(chatModel.Seller.Id, result.Seller.Id);
            Assert.Equal(new List<Message>(), result.Messages );
        }

        [Fact]
        public async Task SendMessage_Success()
        {
            Guid chatId = Guid.NewGuid();
            
            var messageModel = new MessageModel()
            {
                SenderId = Guid.NewGuid(),
                Text = "Dit is een bericht!"
            };

            var newMessage = new Message()
            {
                SenderId = messageModel.SenderId,
                Text = messageModel.Text,
                TimeStamp = DateTime.Now,
                Read = false
            };

            var chat = new Chat()
            {
                Id = chatId,
                Buyer = new User {Name = "test@test.test", Id = Guid.NewGuid()},
                Seller = new User {Name = "my@email.be", Id = Guid.NewGuid()},
                Messages = new List<Message>()

            };
            //updatedChat.Messages.Add(newMessage);
            
            _repository.Setup(x => x.GetChat(chatId)).ReturnsAsync(chat);
            _repository.Setup(x => x.Update(chatId, chat)).ReturnsAsync(chat);

            var result = await _chatService.SendMessage(chatId, messageModel);

            Assert.Equal(chat.Messages.Count, result.Messages.Count);
            Assert.Equal(chat.Id, chatId);
        }
        
    }
}