using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunicationService.Domain;
using CommunicationService.Exceptions;
using CommunicationService.Hubs;
using CommunicationService.Models;
using CommunicationService.Repository;
using CommunicationService.Services;
using Moq;
using Xunit;

namespace CommunicationServiceTest.ServiceTests
{
    public class ChatServiceTest
    {
        private readonly IChatService _chatService;
        private readonly Mock<IChatRepository> _repository;
        private readonly Mock<ILiveChatService> _liveChatService;

        public ChatServiceTest()
        {
            _repository = new Mock<IChatRepository>();
            _liveChatService = new Mock<ILiveChatService>();
            
            _chatService = new ChatService(_repository.Object, _liveChatService.Object);
        }

        [Fact]
        public async Task InitializeChat_Success_ReturnsCreatedChatObject()
        {
            var buyerId = Guid.NewGuid();
            var sellerId = Guid.NewGuid();
            var initModel = new InitializeModel()
            {
                Buyer = new User {Email = "test@test.test", Id = buyerId},
                Seller = new User {Email = "my@email.be", Id = sellerId}
            };
            
            var chatModel = new Chat()
            {
                Buyer = new User {Email = "test@test.test", Id = buyerId},
                Seller = new User {Email = "my@email.be", Id = sellerId},
                Messages = new List<Message>()
            };
            var returnChat = new Chat()
            {
                Id = Guid.NewGuid(),
                Buyer = new User {Email = "test@test.test", Id = buyerId},
                Seller = new User {Email = "my@email.be", Id = sellerId},
                Messages = new List<Message>()
            };

            _repository.Setup(x => x.Create(It.IsAny<Chat>())).ReturnsAsync(returnChat);

            var result = await _chatService.InitializeChat(initModel);

            Assert.Equal(chatModel.Buyer.Email, result.Buyer.Email);
            Assert.Equal(chatModel.Seller.Id, result.Seller.Id);
            Assert.Equal(new List<Message>(), result.Messages );
        }
      
        [Fact]
        public async Task GetUserChats_Success()
        {
            var buyerId1 = Guid.NewGuid();
            var sellerId1 = Guid.NewGuid();
            var sellerId2 = Guid.NewGuid();

            var chat1 = new Chat()
            {
                Id = Guid.NewGuid(),
                Buyer = new User {Email = "test@test.test", Id = buyerId1},
                Seller = new User {Email = "my@email.be", Id = sellerId1},
                Messages = new List<Message>()
            };
            var chat2 = new Chat()
            {
                Id = Guid.NewGuid(),
                Buyer = new User {Email = "test@test.test", Id = buyerId1},
                Seller = new User {Email = "my@email.be", Id = sellerId2},
                Messages = new List<Message>()
            };
            var chatList = new List<Chat> {chat1, chat2};

            _repository.Setup(x => x.GetUserChats(buyerId1)).ReturnsAsync(chatList);

            var result = await _chatService.GetUserChats(buyerId1);

            Assert.Equal(2, result.Count     );
        }

        [Fact]
        public async Task GetUserChats_NotFound()
        {
            var result = await Assert.ThrowsAsync<NotFoundException>(() => _chatService.GetUserChats(Guid.NewGuid()));

            Assert.IsType<NotFoundException>(result);
        }

        
        [Fact]
        public async Task GetChatMessages_Success()
        {
            var message1 = new Message()
            {
                Id = Guid.NewGuid(),
                SenderId = Guid.NewGuid(),
                Text = "textje1",
                TimeStamp = DateTime.Now,
                Read = false
            };
            var message2 = new Message()
            {
                Id = Guid.NewGuid(),
                SenderId = Guid.NewGuid(),
                Text = "textje2",
                TimeStamp = DateTime.Now,
                Read = false
            };
            
            var messageList = new List<Message> {message1, message2};

            _repository.Setup(x => x.GetChatMessages(It.IsAny<Guid>())).ReturnsAsync(messageList);

            var result = await _chatService.GetChatMessages(Guid.NewGuid());

            Assert.Equal(2, result.Count);
        }  
        
        [Fact]
        public async Task GetChatMessages_NotFound()
        {
            var result = await Assert.ThrowsAsync<NotFoundException>(() => _chatService.GetChatMessages(Guid.NewGuid()));

            Assert.IsType<NotFoundException>(result);
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
                Buyer = new User {Email = "test@test.test", Id = Guid.NewGuid()},
                Seller = new User {Email = "my@email.be", Id = Guid.NewGuid()},
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