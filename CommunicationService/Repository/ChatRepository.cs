using System.Threading.Tasks;
using CommunicationService.DataStoreSettings;
using CommunicationService.Domain;
using CommunicationService.Models;
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

    }
}