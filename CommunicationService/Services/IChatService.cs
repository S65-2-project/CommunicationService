using System;
using System.Threading.Tasks;
using CommunicationService.Domain;
using CommunicationService.Models;

namespace CommunicationService.Services
{
    public interface IChatService
    {
        /// <summary>
        /// Initializes new chat between users
        /// </summary>
        /// <param name="initModel"></param>
        /// <returns>Chat</returns>
        Task<Chat> InitializeChat(InitializeModel initModel);

        /// <summary>
        /// Adds a new message to the chat datbase object.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="message"></param>
        /// <returns>Chat</returns>
        Task<Chat> SendMessage(Guid id, MessageModel message);
    }
}