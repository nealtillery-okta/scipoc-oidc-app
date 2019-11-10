using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Forms45OIDCExample.TokenFlow;
using Newtonsoft.Json.Linq;
using Okta.Core;
using System.Web.Caching;
using Okta.Core.Models;
using Okta.Core.Clients;

namespace Forms45OIDCExample
{
    public partial class LoginFlow : System.Web.UI.Page
    {
        private const int LOGIN_STEP = 0;
        private const int SECURE_WORD_STEP = 1;
        private const int PASSWORD_STEP = 2;
        private const int FINAL_STEP = 3;
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnNotYou_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/LoginFlow.aspx");
        }
        protected void wizLoginFlow_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {

        }




        protected void wizLoginFlow_ActiveStepChanged(object sender, EventArgs e)
        {
            var idx = this.wizLoginFlow.ActiveStepIndex - 1;  //index is set to the page its moving to
            switch (idx)
            {
                case LOGIN_STEP:
                    CaptureLogin(this.tbLoginName.Text);
                    break;
                case SECURE_WORD_STEP:
                    break;
                case PASSWORD_STEP:
                    CheckPassword(this.tbLoginName.Text, this.tbPassword.Text);
                    break;
            }
        }
        #endregion
        private void CheckPassword(string login, string password)
        {
            ResetErrors();
            try
            {
                AuthResponse result = Factory.AuthnClient.Authenticate(login, password);

                if (string.IsNullOrEmpty(result.SessionToken))
                {
                    //todo: error handling
                    ShowError($"Login was unsuccessful because {result.Status}");
                }

                var tokProvider = new TokenFlow.Provider();
                var auth_url = tokProvider.GetAuthorizeUrl(result.SessionToken);

                Response.Redirect(auth_url);
            }
            catch (OktaAuthenticationException o)
            {
                this.wizLoginFlow.MoveTo(this.stepPassword);
                ShowError(o.ErrorSummary);
            }
            catch (Exception e)
            {
                this.wizLoginFlow.MoveTo(this.stepPassword);
                var msg = e.InnerException != null ? e.InnerException.Message : e.Message;
                ShowError($"There was an error in your request {msg}");
            }


            //TODO: DO SOMETHING
        }
        protected JObject UserObject
        {
            get
            {
                return (JObject)Session["UserObject"];
            }
            set
            {
                Session["UserObject"] = value;
            }
        }
        private void CaptureLogin(string login)
        {
            ResetErrors();
            try
            {
                var user = Factory.UserClient.GetByUsername(login);
                if (user == null)
                {
                    // todo raise a proper error
                    ShowError("User not found");
                }
                var cl = Factory.AppUserClient;
                var appUser = Factory.AppUserClient.Where(x => x.Credentials.UserName.Equals(login)).FirstOrDefault();

                var sw = "NOT FOUND";

                var json = appUser.ToJson();
                UserObject = JObject.Parse(appUser.ToJson());
                sw = UserObject["profile"]["secureWord"].ToString();

                this.imgSecureImage.ImageUrl = $"/images/secure/{UserObject["profile"]["secureImage"]}.png";
                this.lblSecureWord.Text = sw;

            }
            catch (Exception e)
            {
                this.wizLoginFlow.MoveTo(this.stepLogin);
                var msg = e.InnerException != null ? e.InnerException.Message : e.Message;
                ShowError($"There was an error in your request {msg}");
            }

        }
        private void ResetErrors()
        {
            this.cvGeneric.IsValid = true;
            this.cvGeneric.ErrorMessage = String.Empty;
        }
        private void ShowError(string message)
        {

            this.cvGeneric.ErrorMessage = message;
            this.cvGeneric.IsValid = false;

        }


    }
}