using System;

namespace CommunicationService.Helper
{
    public interface IJwtIdClaimReaderHelper
    {
        /// <summary>
        /// Extracts a userId from a given token
        /// </summary>
        /// <param name="jwt">the input token</param>
        /// <returns>userId as Guid</returns>
        public Guid GetUserIdFromToken(string jwt);
    }
}
