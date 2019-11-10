using Okta.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Forms45OIDCExample
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        public string RecoveryToken { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                PostBackEvents();
            }
            else
            {
                NonPostBackEvents();
            }
        }

        private void PostBackEvents()
        {
            this.RecoveryToken = Request["ott"];
        }
        private void NonPostBackEvents()
        {
            this.RecoveryToken = Request["ott"];
            this.tbOTT.Value = this.RecoveryToken;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/");
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string newPassword = this.tbPassword.Text;
            string confirmPassword = this.tbConfirmPassword.Text;

            try
            {
                // validate OTT (recoveryToken) via /authn API
                // then call update user to set the password
                AuthResponse response = Factory.AuthnClient.ValidateToken(this.RecoveryToken);
                string userId = response.Embedded.User.Id;
                string username = response.Embedded.User.Profile.Login;
                Factory.UserClient.SetPassword(userId, newPassword);
                AuthResponse auth = Factory.AuthnClient.Authenticate(username, newPassword);
                string sessionToken = auth.SessionToken;
                TokenFlow.Provider tokProvider = new TokenFlow.Provider();
                string auth_url = tokProvider.GetAuthorizeUrl(sessionToken);
                Response.Redirect(auth_url);
            }

            catch (Okta.Core.OktaException ox)
            {
                if (ox.ErrorCauses != null)
                {
                    var errs = new List<string>();
                    errs.AddRange(ox.ErrorCauses.Select(x => x.ErrorSummary));

                    this.lblMessage.Text = $"Failed.  Attempt to reset password failed because {string.Join(",", errs)}.";
                }
                else
                {
                    this.lblMessage.Text = $"Failed.  Attempt to reset password failed because {ox.Message}.";
                }

            }
            catch (Exception ex)
            {

                this.lblMessage.Text = $"Failed.  Attempt to reset password failed because {ex.Message}.";
            }

        }
    }
}