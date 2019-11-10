using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Forms45OIDCExample
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.EnsureAccess();
            if (Page.IsPostBack)
            {
                PostBackEvents();
            } else
            {
                NonPostBackEvents();
            }
        }
        private void PostBackEvents()
        {

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
                Factory.UserClient.ChangePassword(this.Master.Profile.ToOktaUser(), this.tbCurrentPassword.Text, this.tbPassword.Text);
                this.Master.ShowPopup("Success!  Password successfully changed.");

            }
            catch (Okta.Core.OktaException ox)
            {
                if (ox.ErrorCauses != null)
                {
                    var errs = new List<string>();
                    errs.AddRange(ox.ErrorCauses.Select(x => x.ErrorSummary));
                   
                    this.Master.ShowPopup($"Failed.  Attempt to change password failed because {string.Join(",", errs)}.");
                } else
                {
                    this.Master.ShowPopup($"Failed.  Attempt to change password failed because {ox.Message}.");
                }
            
            }
            catch (Exception ex)
            {
               
                this.Master.ShowPopup($"Failed.  Attempt to change password failed because {ex.Message}.");
            }
             

        }
    }
}