using System.Threading.Tasks;
using CommunicationService.Domain;

namespace CommunicationService.Repository
{
    public interface IChatRepository
    {
        Task<Chat> Create(Chat chat);
    }
}