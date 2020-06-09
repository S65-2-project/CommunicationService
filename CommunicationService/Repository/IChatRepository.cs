using System;
using System.Threading.Tasks;
using CommunicationService.Domain;

namespace CommunicationService.Repository
{
    public interface IChatRepository
    {
        /// <summary>
        /// Initializes new chat between users
        /// </summary>
        /// <param name="initModel"></param>
        /// <returns>Chat</returns>
        Task<Chat> Create(Chat chat);

        /// <summary>
        /// Adds a new message to the Chat database object
        /// </summary>
        /// <param name="id"></param>
        /// <param name="chat"></param>
        /// <returns>Chat</returns>
        Task<Chat> Update(Guid id, Chat chat);
        
        /// <summary>
        /// Gets a chat object from the datbase
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Chat</returns>
        Task<Chat> GetChat(Guid id);

        /// <summary>
        /// Finds a chat where buyer and seller match parameters
        /// </summary>
        /// <param name="buyer"></param>
        /// <param name="seller"></param>
        /// <returns>Chat</returns>
        Task<Chat> FindChat(User buyer, User seller);
    }
}