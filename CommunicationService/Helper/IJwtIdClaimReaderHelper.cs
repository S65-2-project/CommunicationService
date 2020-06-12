using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunicationService.Helper
{
    public interface IJwtIdClaimReaderHelper
    {
        public Guid getUserIdFromToken(string jwt);
    }
}
