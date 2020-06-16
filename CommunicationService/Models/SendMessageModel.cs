using System;
using CommunicationService.Domain;

namespace CommunicationService.Models
{
    public class LiveMessageModel
    {
        public Guid ChatId { get; set; }
        public Message Message { get; set; } 
    }
}