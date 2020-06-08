using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace CommunicationService.Domain
{
    public class Chat
    {
        [BsonId]
        public Guid Id { get; set; }
        public User Seller { get; set; }
        public User Buyer { get; set; }
        public List<Message> Messages { get; set; }
    }
}