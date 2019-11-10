using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forms45OIDCExample
{
    public static class Extensions
    {
        public static Okta.Core.Models.User ToOktaUser(this ProfileModel prof)
        {
            return new Okta.Core.Models.User
            {
                Id = prof.Id,
                Profile = new Okta.Core.Models.UserProfile
                {
                    Email = prof.PrimaryEmail,
                    FirstName = prof.FirstName,
                    LastName = prof.LastName,
                    Login = prof.Login,
                    SecondaryEmail = prof.SecondaryEmail,
                    MobilePhone = prof.MobilePhone
                }

            };

        }
    }
}