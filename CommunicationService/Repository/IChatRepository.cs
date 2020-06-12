using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunicationService.Domain;

namespace CommunicationService.Repository
{
    public interface IChatRepository
    {
        /// <summary>
        /// Initializes new chat between users
        /// </summary>
        /// <param name="chat"></param>
        /// <returns>Chat</returns>
        Task<Chat> Create(Chat chat);
        
        /// <summary>
        /// Gets all chats of a user
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <returns>List with all the chats</returns>
        Task<List<Chat>> GetUserChats(Guid id);
        
        /// <summary>
        /// Get all messages in a single chat
        /// </summary>
        /// <param name="id">Id of the chat</param>
        /// <returns>List of all the messages</returns>
        Task<List<Message>> GetChatMessages(Guid id);
        
        /// <summary>
        /// Reads all new messages for the user in a chat
        /// </summary>
        /// <param name="id">id of the chat</param>
        /// <param name="userId">id of the user</param>
        /// <returns></returns>
        Task ReadChat(Guid id, Guid userId);
    }
}