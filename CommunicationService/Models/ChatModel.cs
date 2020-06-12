using System;
using CommunicationService.Domain;

namespace CommunicationService.Models
{
    public class ChatModel
    {
        public Guid Id { get; set; }
        public User Seller { get; set; }
        public User Buyer { get; set; }
        public Message Message { get; set; }
        public int Unread { get; set; }
    }
}