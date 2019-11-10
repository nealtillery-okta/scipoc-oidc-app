using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Web.Security;
using Microsoft.Owin.Security.Cookies;
using System.Security.Authentication;
using System.Net;
using System.Web.Caching;
using Forms45OIDCExample.TokenFlow;

namespace Forms45OIDCExample
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lbLogOut.Visible = false;
            //this.lnkLogin.Visible = true;

            //this is specific to the login flow
            if (Session["Tokens"] != null)
            {
             //   this.lnkLogin.Visible = false;
                this.lbLogOut.Visible = true;
            }

            this.genUserLink.Visible = IsAuthenticated;
            if (IsAuthenticated)
            {
                this.hypProfile.Text = $"<span class =\"glyphicon glyphicon-user\"></span>&nbsp; {System.Threading.Thread.CurrentPrincipal.Identity.Name}";
                
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                const SslProtocols _Tls12 = (SslProtocols)0x00000C00;
                const SecurityProtocolType Tls12 = (SecurityProtocolType)_Tls12;
                ServicePointManager.SecurityProtocol = Tls12;
                HttpContext.Current.GetOwinContext().Authentication.Challenge(
                  new AuthenticationProperties { RedirectUri = "/" },
                  OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            Session["Tokens"] = null;
            //this.lnkLogin.Visible = true;
        }
        /// <summary>
        /// Method will redirect the session to Login when the user fails an authentication check
        /// </summary>
        public void EnsureAccess()
        {
            if (!IsAuthenticated)
            {
                Session["DestinationUrl"] = Request.Url;
                Response.Redirect("~/LoginFlow.aspx");
            }
        }
        public bool IsAuthenticated
        {
            get
            {
                return Request.IsAuthenticated || Session["Tokens"] != null;
            }
        }

        public ProfileModel Profile
        {
            get
            {
                if (!IsAuthenticated)
                {
                    return null;
                }

                if (Session["Tokens"] != null)
                {
                    return  new ProfileModel((TokenModel)Session["Tokens"]);
                } else
                {
                    //build from claims
                    var prov = new Provider();
                    return new ProfileModel(prov.GetTokensFromCurrentPrincipal());


                }
              
            }
        }
        protected void lbLogOut_Click(object sender, EventArgs e)
        {
            Session["Tokens"] = null;
            Response.Redirect("~/");
        }

        public void ShowPopup(string message)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showPopupMessage", $" alert(' {message} ');", true);
        }
    }
}