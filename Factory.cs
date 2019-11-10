using Okta.Core.Clients;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.Cookies;
using System.Security.Authentication;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace Forms45OIDCExample
{
    public static class Factory
    {
        private static readonly string ClientId = ConfigurationManager.AppSettings["okta:ClientId"];

        private static readonly string Token = ConfigurationManager.AppSettings["okta:token"];
        private static readonly string Org = ConfigurationManager.AppSettings["okta:Org"];
        
        
        private static  OktaClient OktaClient
        {
            get
            {
                const SslProtocols _Tls12 = (SslProtocols)0x00000C00;
                const SecurityProtocolType Tls12 = (SecurityProtocolType)_Tls12;
                ServicePointManager.SecurityProtocol = Tls12;
                return new OktaClient(Token, new Uri(Org));
            }
        }
        public static UsersClient UserClient
        {
            get
            {
                return OktaClient.GetUsersClient();
            }
        }
        public static AppsClient AppClient
        {
            get
            {
                return OktaClient.GetAppsClient();
                
            }
        }
        public static AppUsersClient AppUserClient
        {
            get
            {

                return OktaClient.GetAppUsersClient(AppDef);


            }
        }
        public static AuthClient AuthnClient
        {
            get
            {
                return OktaClient.GetAuthClient();
            }
        }
        public static Okta.Core.Models.App AppDef
        {
            get
            {
                var apps = OktaClient.GetAppsClient();
               
                return apps.Where(x => x.Id.Equals(ClientId)).FirstOrDefault();
            }
        }

        public static async Task<HttpResponseMessage> SecureItemUpdate(string userId, string secureWord, string secureImage)
        {
            var job = new JObject();
            var par = new JObject();
            
           
            job.Add("secureWord", secureWord);
            job.Add("secureImage", secureImage);
            par.Add("profile",job);
            return await OktaClient.BaseClient.PostAsync( $"/api/v1/apps/{ClientId}/users/{userId}",

                par.ToString()
           );
        }
    }
}