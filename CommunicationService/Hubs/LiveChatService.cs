using System;
using CommunicationService.Models;
using Microsoft.AspNetCore.SignalR;

namespace CommunicationService.Hubs
{
    public class LiveChatService : ILiveChatService
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public LiveChatService(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async void SendPrivateMessage(LiveMessageModel model, Guid user)
        {
            await _hubContext.Clients.User(user.ToString()).SendAsync("ReceiveMessage", model);
        }
    }
}