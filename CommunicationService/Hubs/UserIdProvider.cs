using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace CommunicationService.Hubs
{
    public class UserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}