using System.Threading.Tasks;
using CommunicationService.Domain;
using CommunicationService.Models;

namespace CommunicationService.Services
{
    public interface IChatService
    {
        Task<Chat> InitializeChat(InitializeModel initModel);
    }
}