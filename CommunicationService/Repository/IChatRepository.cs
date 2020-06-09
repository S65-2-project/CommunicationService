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
        /// <param name="initModel"></param>
        /// <returns>Chat</returns>
        Task<Chat> Create(Chat chat);

        Task<List<Chat>> GetUserChats(Guid id);
        Task<List<Message>> GetChatMessages(Guid id);
    }
}