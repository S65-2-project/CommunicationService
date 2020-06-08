using System;
using System.Collections.Generic;

namespace CommunicationService.Domain
{
    public class Chat
    {
        public Guid Id { get; set; }
        public User Seller { get; set; }
        public User Buyer { get; set; }
        public List<Message> Messages { get; set; }
    }
}