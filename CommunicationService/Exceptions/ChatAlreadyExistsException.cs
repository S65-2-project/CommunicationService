using System;

namespace CommunicationService.Exceptions
{
    public class ChatAlreadyExistsException : Exception
    {
        public ChatAlreadyExistsException(Guid id) 
            : base("A Chat with this ID already Exists. ID= " + id)
        {
        }

    }
}