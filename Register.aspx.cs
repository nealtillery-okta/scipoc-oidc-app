using Okta.Core.Clients;
using Okta.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Forms45OIDCExample
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UserProfileFields.ProfileMode = Forms45OIDCExample.Controls.UserProfileFields.UserProfileMode.Create;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            HttpResponseMessage resp;
            var prof = this.UserProfileFields.GetValues();
            var step = "Creating Profile";
            try
            {
                LoginCredentials credentials = new LoginCredentials
                {
                    Password = new Password
                    {
                        Value = prof.Password
                    }
                };
                User new_user = prof.ToOktaUser();
                new_user.Credentials = credentials;

                User user = Factory.UserClient.Add(new_user);
                string userId = user.Id;

                step = "Adding Secure Items";
                Task.Run(async () =>
                {
                    resp = await Factory.SecureItemUpdate(userId, prof.SecureWord, prof.SecureImage);
                }
                ).Wait();

                Response.Redirect("~/Success");
            }
            catch (Exception ex)
            {
                this.Master.ShowPopup($"Failed. Error received {step}.  {ex.Message}");
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}