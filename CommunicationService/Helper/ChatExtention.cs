using System;
using System.Collections.Generic;
using System.Linq;
using CommunicationService.Domain;
using CommunicationService.Models;

namespace CommunicationService.Helper
{
    public static class ChatExtention
    {
        public static ChatModel WithoutMessages(this Chat chat, Guid id)
        {
            var model = new ChatModel
            {
                Id = chat.Id,
                Buyer = chat.Buyer,
                Seller = chat.Seller,
                Unread = 0
            };
            if (chat.Messages.Count > 0)
            {
                model.Message = chat.Messages[^1];
                model.Unread = chat.Messages.Count(p => !p.Read && p.SenderId != id);
            }
            return model;
        }

        public static List<ChatModel> WithoutMessages(this List<Chat> chats, Guid id)
        {
            return chats.Select(x => x.WithoutMessages(id)).ToList();
        }
    }
}