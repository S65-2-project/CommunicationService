﻿using System;
using System.Collections.Generic;
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
      
        /// Adds a new message to the chat datbase object.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="message"></param>
        /// <returns>Chat</returns>
        Task<Chat> SendMessage(Guid id, MessageModel message);
    }
}