using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace CommunicationService.Helper
{
    public class JwtIdClaimReaderHelper : IJwtIdClaimReaderHelper
    {
        public Guid getUserIdFromToken(string jwt)
        {
            var token = new JwtSecurityToken(jwt.Replace("Bearer ", String.Empty));
            var idclaim = Guid.Parse((string)token.Payload["unique_name"]);
            return idclaim;
        }
    }
}
