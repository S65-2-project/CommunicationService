using CommunicationService.Domain;

namespace CommunicationService.Models
{
    public class InitializeModel
    {
        public User Seller { get; set; }
        public User Buyer { get; set; }
    }
}