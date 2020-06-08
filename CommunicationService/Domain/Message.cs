using System;

namespace CommunicationService.Domain
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool Read { get; set; }
    }
}