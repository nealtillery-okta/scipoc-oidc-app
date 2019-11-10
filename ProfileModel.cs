using Forms45OIDCExample.TokenFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forms45OIDCExample
{
    public class ProfileModel
    {
        public ProfileModel() { }
        public ProfileModel(TokenModel model)
        {
            //TODO: build properties from the incoming tokens
            Id = model.IDToken.Subject;
            Login = model.AccessToken.Subject;
            FirstName = model.IDToken.Claims.First(x => x.Type == "firstName").Value;
            LastName = model.IDToken.Claims.First(x => x.Type == "lastName").Value;
            PrimaryEmail = model.IDToken.Claims.First(x => x.Type == "email").Value;
            SecondaryEmail = model.IDToken.Claims.First(x => x.Type == "secondaryEmail").Value;
            MobilePhone = model.IDToken.Claims.First(x => x.Type == "mobilePhone").Value;
            SecureWord = model.IDToken.Claims.First(x => x.Type == "secureWord").Value;
            SecureImage = model.IDToken.Claims.First(x => x.Type == "secureImage").Value;

        }
        public string Id { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PrimaryEmail { get; set; }
        public string SecondaryEmail { get; set; }
        public string MobilePhone { get; set; }
        public string SecureWord { get; set; }
        public string SecureImage { get; set; }
        public string Password { get; set; }
    }
}