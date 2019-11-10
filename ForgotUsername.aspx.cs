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
    public partial class ForgotUsername : System.Web.UI.Page
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
            this.lblEmailAddress.Visible = false;
            this.tbEmailAddress.Visible = false;
            this.btnCancel.Visible = false;
            this.btnSubmit.Visible = false;
        }
        private void NonPostBackEvents()
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                User user = Factory.UserClient.Where<User>(u => u.Profile.Email.Equals(this.tbEmailAddress.Text)).FirstOrDefault();
                string to = user.Profile.Email;
                string username = user.Profile.Login;
                this.SendEmail(to, username);
                this.lblMessage.Text = "Check your email, we just sent you your username.";
            }
            catch (Okta.Core.OktaException ox)
            {
                if (ox.ErrorCauses != null)
                {
                    var errs = new List<string>();
                    errs.AddRange(ox.ErrorCauses.Select(x => x.ErrorSummary));

                    this.lblMessage.Text = $"Failed.  Attempt to lookup username failed because {string.Join(",", errs)}.";
                }
                else
                {
                    this.lblMessage.Text = $"Failed.  Attempt to lookup username failed because {ox.Message}.";
                }

            }
            catch (Exception ex)
            {

                this.lblMessage.Text = $"Failed.  Attempt to lookup username failed because {ex.Message}.";
            }

        }

        private void SendEmail(string to, string username)
        {
            // start by creating a new SparkPost transmission
            var transmission = new Transmission();
            transmission.Content.From.Email = "principal-poc@mail.thoraxstudios.com";
            transmission.Content.Subject = "Your Principal POC Username";
            string body = "Your username for the Principal POC is below.\n";
            body += username + "\n\n";
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