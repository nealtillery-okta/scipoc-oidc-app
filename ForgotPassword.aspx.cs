using Okta.Core.Clients;
using Okta.Core.Models;
using SparkPost;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Forms45OIDCExample
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
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
            this.lblUsername.Visible = false;
            this.tbUsername.Visible = false;
            this.btnCancel.Visible = false;
            this.btnResetEmail.Visible = false;
            this.btnResetSms.Visible = false;
            this.btnVerifySms.Visible = true;
            this.lblSmsCode.Visible = true;
            this.tbSmsCode.Visible = true;
        }
        private void NonPostBackEvents()
        {
            this.lblSmsCode.Visible = false;
            this.tbSmsCode.Visible = false;
            this.btnVerifySms.Visible = false;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/");
        }

        protected void btnResetSms_Click(object sender, EventArgs e)
        {
            User user = Factory.UserClient.Where<User>(u => u.Profile.Login.Equals(this.tbUsername.Text)).FirstOrDefault();
            UserFactorsClient factorsClient = Factory.UserClient.GetUserFactorsClient(user);
            Factor smsFactor = factorsClient.First(x => x.FactorType == FactorType.Sms);
            ChallengeResponse response = factorsClient.BeginChallenge(smsFactor);
            this.lblMessage.Text = "An SMS message was sent to your phone number on file.";
        }
        protected void btnVerifySms_Click(object sender, EventArgs e)
        {
            string passcode = this.tbSmsCode.Text;
            User user = Factory.UserClient.Where<User>(u => u.Profile.Login.Equals(this.tbUsername.Text)).FirstOrDefault();
            UserFactorsClient factorsClient = Factory.UserClient.GetUserFactorsClient(user);
            Factor smsFactor = factorsClient.First(x => x.FactorType == FactorType.Sms);
            MfaAnswer answer = new MfaAnswer { Passcode = passcode };
            ChallengeResponse response = factorsClient.CompleteChallenge(smsFactor, answer);
            if (response.FactorResult == "SUCCESS" )
            {
                Uri uri = Factory.UserClient.ForgotPassword(user, false);
                string recoveryToken = this.GetRecoveryTokenFromUri(uri);
                string resetLink = string.Format("{0}/ResetPassword?ott={1}", "http://localhost:8080", recoveryToken);
                Response.Redirect(resetLink);
            }
            else
            {
                // TODO what to do if the verification fails?
                lblMessage.Text = string.Format("Result {0}: {1}", response.FactorResult, response.FactorResultMessage);
            }
        }

        // reset password with email verification
        protected void btnResetEmail_Click(object sender, EventArgs e)
        {
            this.lblSmsCode.Visible = false;
            this.tbSmsCode.Visible = false;

            try
            {
                User user = Factory.UserClient.Where<User>(u => u.Profile.Login.Equals(this.tbUsername.Text)).FirstOrDefault();
                Uri uri = Factory.UserClient.ForgotPassword(user, false);
                string recoveryToken = this.GetRecoveryTokenFromUri(uri);
                string resetLink = string.Format("{0}/ResetPassword?ott={1}", "http://localhost:8080", recoveryToken);
                // send the reset link to the email on file
                this.SendEmail(user.Profile.Email, resetLink);
                this.lblMessage.Text = "An email has been sent to the address on file with instructions on how to recover your password.";
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
        private string GetRecoveryTokenFromUri(Uri uri)
        {
            string url = uri.ToString();
            string[] parts = url.Split(new char[] { '/' });
            string ott = parts[parts.Length - 1];
            return ott;
        }
        private void SendEmail(string to, string resetLink)
        {
            // start by creating a new SparkPost transmission
            var transmission = new Transmission();
            transmission.Content.From.Email = "principal-poc@mail.thoraxstudios.com";
            transmission.Content.Subject = "Recover your forgotten password";
            string body = "You asked to recover your password.\n";
            body += "Please click the link below to finish the recovery process.\n";
            body += resetLink + "\n\n";
            transmission.Content.Text = body;

            // add recipients who will receive your email
            var recipient = new Recipient
            {
                Address = new Address { Email = to }
            };
            transmission.Recipients.Add(recipient);

            // create a new API client using your API key
            string apiKey = ConfigurationManager.AppSettings["sparkpost:ApiKey"];
            var client = new Client(apiKey);

            // if you do not understand async/await, use the sync sending mode:
            client.CustomSettings.SendingMode = SendingModes.Sync;
            var response = client.Transmissions.Send(transmission);
        }
    }
}
