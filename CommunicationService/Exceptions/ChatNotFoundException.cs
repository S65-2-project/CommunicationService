using System;

namespace CommunicationService.Exceptions
{
    public class ChatNotFoundException : Exception
    {
        public ChatNotFoundException(Guid id) : base("Chat with ID: " + id + " was not found")
        {
        }
    }
}