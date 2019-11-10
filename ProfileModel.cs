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
            Login = model.IDToken.Claims.First(x => x.Type == "preferred_username").Value;
            PrimaryEmail = model.IDToken.Claims.First(x => x.Type == "email").Value;
            var fullname = model.IDToken.Claims.First(x => x.Type == "name").Value;
            //FirstName = model.IDToken.Claims.First(x => x.Type == "firstName").Value;
            //LastName = model.IDToken.Claims.First(x => x.Type == "lastName").Value;
            FirstName = fullname.Split(' ')[0];
            LastName = fullname.Split(' ')[1];
            //SecondaryEmail = model.IDToken.Claims.First(x => x.Type == "secondaryEmail").Value;
            SecondaryEmail = PrimaryEmail;
            //MobilePhone = model.IDToken.Claims.First(x => x.Type == "mobilePhone").Value;
            MobilePhone = "Missing";
            //SecureWord = model.IDToken.Claims.First(x => x.Type == "secureWord").Value;
            //SecureImage = model.IDToken.Claims.First(x => x.Type == "secureImage").Value;
            SecureWord = "Missing";
            SecureImage = "ball";
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