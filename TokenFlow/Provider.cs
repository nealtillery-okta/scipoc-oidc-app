using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Forms45OIDCExample.TokenFlow
{
    public class Provider
    {
        private readonly string _clientId = ConfigurationManager.AppSettings["okta:ClientId"];

        private readonly string _redirectUri = ConfigurationManager.AppSettings["okta:RedirectUriAlt"];
        private readonly string _authority = ConfigurationManager.AppSettings["okta:OrgUri"];
        private readonly string _clientSecret = ConfigurationManager.AppSettings["okta:ClientSecret"];

        public string GetAuthorizeUrl(string sessionToken)
        {
            var builder = new UriBuilder(_authority + "/v1/authorize");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["client_id"] = _clientId;
            query["response_type"] = "code";
            query["response_mode"] = "query";
            query["redirect_uri"] = _redirectUri;
            query["nonce"] = Guid.NewGuid().ToString();
            query["scope"] = "openid email";
            query["sessionToken"] = sessionToken;
            query["state"] = Guid.NewGuid().ToString();
            builder.Query = query.ToString();

            return builder.ToString();
        }
        public async Task<TokenModel> ObtainTokens(string authCode)
        {
            var tm = new TokenModel();
            var url = _authority + "/v1/token";
            var parameters = new Dictionary<string, string> {
                { "code", authCode },
                { "client_id", _clientId },
                { "client_secret", _clientSecret },
                { "grant_type", "authorization_code" },
                { "redirect_uri", _redirectUri },
                { "accept", "application/json" }
                
            };
            var encodedContent = new FormUrlEncodedContent(parameters);
            var client = new HttpClient();
            var response = await client.PostAsync(url, encodedContent).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var job = JObject.Parse(responseContent);


                var accessToken = await ValidateToken(job["access_token"].ToString(), "api://default");
                var idToken = await ValidateToken(job["id_token"].ToString(), _clientId);


                if (accessToken == null)
                {
                    Console.WriteLine("Invalid Access token");
                }
                else
                {
                    // Additional validation...
                    tm.AccessToken = accessToken;
                }
                if (idToken == null)
                {
                    Console.WriteLine("Invalid ID token");
                }
                else
                {
                    // Additional validation...
                    tm.IDToken = idToken;
                }
            }
            return tm;
        }
        public TokenModel GetTokensFromCurrentPrincipal()
        {
            var idTok = System.Security.Claims.ClaimsPrincipal.Current.FindFirst("id_token");
            var accTok = System.Security.Claims.ClaimsPrincipal.Current.FindFirst("access_token");

            var tm = new TokenModel();
            Task.Run(async () =>
           {
               var accessToken = await ValidateToken(accTok.Value, "api://default");
               var idToken = await ValidateToken(idTok.Value, _clientId);

               if (accessToken == null)
               {
                   Console.WriteLine("Invalid Access token");
               }
               else
               {
                    // Additional validation...
                    tm.AccessToken = accessToken;
               }
               if (idToken == null)
               {
                   Console.WriteLine("Invalid ID token");
               }
               else
               {
                    // Additional validation...
                    tm.IDToken = idToken;
               }
           }
            ).Wait();
          

            return tm;
        }
       
        private async Task<JwtSecurityToken> ValidateToken(string token, string audience)
        {
            var issuer = _authority;

            var configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                issuer + "/.well-known/oauth-authorization-server",
                new OpenIdConnectConfigurationRetriever(),
                new HttpDocumentRetriever());

            return  await TokenHelper.ValidateToken(token, issuer, configurationManager, audience);
        }

    }
}