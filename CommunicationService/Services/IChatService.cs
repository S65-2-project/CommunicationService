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
    }
}