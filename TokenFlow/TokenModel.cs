using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Web;

namespace Forms45OIDCExample.TokenFlow
{
    public class TokenModel
    {
        public JwtSecurityToken AccessToken { get; set; }
        public JwtSecurityToken IDToken { get; set; }
    }
}