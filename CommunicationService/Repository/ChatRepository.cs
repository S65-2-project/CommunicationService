using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunicationService.DataStoreSettings;
using CommunicationService.Domain;
using MongoDB.Driver;

namespace CommunicationService.Repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly IMongoCollection<Chat> _chat;

        public ChatRepository(IChatDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _chat = database.GetCollection<Chat>(settings.ChatCollectionName);
        }

        public async Task<Chat> Create(Chat chat)
        {
            await _chat.InsertOneAsync(chat);
            return chat;
        }

        public async Task<List<Chat>> GetUserChats(Guid id)
        {
            return await _chat.Find(c => c.Buyer.Id == id || c.Seller.Id == id).ToListAsync();
        }
        
        public async Task<List<Message>> GetChatMessages(Guid id)
        {
            var chat = await _chat.Find(c => c.Id == id).FirstOrDefaultAsync();
            return chat.Messages.OrderBy(p => p.TimeStamp).ToList();
        }

        
        public async Task ReadChat(Guid id, Guid userId)
        {
            var chat = await _chat.Find(c => c.Id == id).FirstOrDefaultAsync();
            foreach (var chatMessage in chat.Messages.Where(chatMessage => chatMessage.SenderId != userId))
            {
                chatMessage.Read = true;
            }

            await _chat.ReplaceOneAsync(p => p.Id == id, chat);
        }
        
        public async Task<Chat> Update(Guid id, Chat chat)
        {
            await _chat.ReplaceOneAsync(x => x.Id == id, chat);
            return chat;
        }

        public async Task<Chat> GetChat(Guid id)
        {
            return await _chat.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Chat> FindChat(User buyer, User seller)
        {
            return await _chat.Find(x => x.Buyer.Id == buyer.Id && x.Seller.Id == seller.Id).FirstOrDefaultAsync();
        }
    }
}