using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Forms45OIDCExample.Controls
{
    public partial class UserProfileFields : System.Web.UI.UserControl
    {
        public enum UserProfileMode
        {
            Create = 0,
            Edit = 1
        }
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

        }


        public void NonPostBackEvents()
        {

            switch (ProfileMode)
            {
                case UserProfileMode.Create:
                    {
                        this.rfvConfirmPassword.Enabled = true;
                        this.rfvPassword.Enabled = true;
                        this.pnlPasswordOps.Visible = true;
                        this.hyChangePassword.Visible = false;
                        //this.lnkEnrollSMS.Visible = false;
                        break;
                    }
                default:
                    this.rfvConfirmPassword.Enabled = false;
                    this.rfvPassword.Enabled = false;
                    this.pnlPasswordOps.Visible = false;
                    this.hyChangePassword.Visible = true;
                    //this.lnkEnrollSMS.Visible = true;
                    break;
            }

            if (Profile == null)
            {
                return;
            }
            this.tbFirstName.Text = Profile.FirstName;
            this.tbLastName.Text = Profile.LastName;
            this.tbLoginEmail.Text = Profile.Login;
            this.tbSecondaryEmail.Text = Profile.SecondaryEmail;
            this.tbPrimaryEmail.Text = Profile.PrimaryEmail;
            this.tbMobilePhone.Text = Profile.MobilePhone;
            this.rbSecureWord.Text = Profile.SecureWord;
            this.ddlSecureImages.SelectedValue = Profile.SecureImage;
            this.hidId.Value = Profile.Id;

        }
        public UserProfileMode ProfileMode
        {
            get; set;
        }

        public ProfileModel GetValues()
        {
            return new ProfileModel
            {
                FirstName = this.tbFirstName.Text,
                LastName = this.tbLastName.Text,
                Login = this.tbLoginEmail.Text,
                SecondaryEmail = this.tbSecondaryEmail.Text,
                PrimaryEmail = this.tbPrimaryEmail.Text,
                MobilePhone = this.tbMobilePhone.Text,
                SecureWord = this.rbSecureWord.Text,
                SecureImage = this.ddlSecureImages.SelectedValue,
                Id = this.hidId.Value,
                Password = this.tbPassword.Text

            };

        }
        public ProfileModel Profile
        {
            get; set;
        }
    }
}