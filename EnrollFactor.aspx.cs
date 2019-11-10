using Okta.Core.Clients;
using Okta.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Forms45OIDCExample
{
    public partial class EnrollFactor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.EnsureAccess();

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
            
        }

        private void NonPostBackEvents()
        {
            this.btnDone.Visible = false;
            string userId = this.Master.Profile.Id;
            User user = Factory.UserClient.Get(userId);

            if (!string.IsNullOrEmpty(user.Profile.MobilePhone))
            {
                // add SMS as a factor, if there's a phone number
                UserFactorsClient factorsClient = Factory.UserClient.GetUserFactorsClient(user);
                Factor to_enroll = new Factor
                {
                    FactorType = FactorType.Sms,
                    Provider = "OKTA",
                    Profile = { PhoneNumber = user.Profile.MobilePhone }
                };
                Factor factor = factorsClient.Enroll(to_enroll);
                this.tbFactorID.Value = factor.Id;
            }
        }

        protected void btnVerifySms_Click(object sender, EventArgs e)
        {
            string factorId = this.tbFactorID.Value;
            string passcode = this.tbSmsCode.Text;
            string userId = this.Master.Profile.Id;
            User user = Factory.UserClient.Get(userId);
            UserFactorsClient factorsClient = Factory.UserClient.GetUserFactorsClient(user);
            Factor factor = factorsClient.GetFactor(factorId);
            Factor response = factorsClient.Activate(factor, passcode);

            if (response.Status == "ACTIVE")
            {
                this.tbSmsCode.Visible = false;
                this.btnVerifySms.Visible = false;
                this.btnCancel.Visible = false;
                this.btnDone.Visible = true;
                lblMessage.Text = "Your phone number has been successfully enrolled";
            }
            else
            {
                // TODO what to do if the verification fails?
                //lblMessage.Text = string.Format("Result {0}: {1}", response.FactorResult, response.FactorResultMessage);
            }
        }

        protected void btnDone_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/");
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Profile.aspx");
        }

    }
}