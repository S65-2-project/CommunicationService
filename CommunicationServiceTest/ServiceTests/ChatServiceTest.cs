using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunicationService.Domain;
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
            var returnmodel = new Chat()
            {
                Id = Guid.NewGuid(),
                Buyer = new User {Name = "test@test.test", Id = buyerId},
                Seller = new User {Name = "my@email.be", Id = sellerId},
                Messages = new List<Message>()
            };


            _repository.Setup(x => x.Create(It.IsAny<Chat>())).ReturnsAsync(returnmodel);

            var result = await _chatService.InitializeChat(initModel);

            Assert.Equal(chatModel.Buyer.Name, result.Buyer.Name);
            Assert.Equal(chatModel.Seller.Id, result.Seller.Id);
            Assert.Equal(new List<Message>(), result.Messages );
        }
    }
}