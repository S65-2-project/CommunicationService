using System;

namespace CommunicationService.Models
{
    public class MessageModel
    {
        public Guid SenderId { get; set; }
        public string Text { get; set; }
    }
}