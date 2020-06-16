using System;
using System.IdentityModel.Tokens.Jwt;

namespace CommunicationService.Helper
{
    public class JwtIdClaimReaderHelper : IJwtIdClaimReaderHelper
    {
        public Guid GetUserIdFromToken(string jwt)
        {
            var token = new JwtSecurityToken(jwt.Replace("Bearer ", String.Empty));
            var idclaim = Guid.Parse((string)token.Payload["unique_name"]);
            return idclaim;
        }
    }
}
