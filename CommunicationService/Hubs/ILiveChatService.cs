using System;
using CommunicationService.Models;

namespace CommunicationService.Hubs
{
    public interface ILiveChatService
    {
        /// <summary>
        /// Sends a private message in a chat
        /// </summary>
        /// <param name="model">Model with message information</param>
        /// <param name="user">The user that has send the message</param>
        public void SendPrivateMessage(LiveMessageModel model, Guid user);
    }
}