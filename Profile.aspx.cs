using Okta.Core;
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
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.EnsureAccess();

            if (Page.IsPostBack)
            {
                //TODO POSTBACK EVENTS
            } else
            {
                NonPostBackEvents();
            }
        }
        private void NonPostBackEvents()
        {
      
            this.UserProfileFields.Profile = this.Master.Profile;
            this.UserProfileFields.ProfileMode = Forms45OIDCExample.Controls.UserProfileFields.UserProfileMode.Edit;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            HttpResponseMessage resp;
            var prof = this.UserProfileFields.GetValues();
            var step = "Updating Profile";
            try
            {
                User okta_user = prof.ToOktaUser();
                Factory.UserClient.Update(okta_user);
                //UserFactorsClient factorsClient = Factory.UserClient.GetUserFactorsClient(okta_user);
                //IEnumerable<Factor> factors = factorsClient.Where(f => f.FactorType == FactorType.Sms);
                //bool factor_enrolled = factors.Count() == 1;

                //// add and activate SMS as a factor, if there's a phone number
                //if (!factor_enrolled && !string.IsNullOrEmpty(prof.MobilePhone))
                //{
                //    Factor to_enroll = new Factor
                //    {
                //        FactorType = FactorType.Sms,
                //        Provider = "OKTA",
                //        Profile = { PhoneNumber = prof.MobilePhone }
                //    };
                //    Factor factor = factorsClient.Enroll(to_enroll);
                //}

                //// there is a factor enrolled, but the phone number has been removed
                //if (factor_enrolled && string.IsNullOrEmpty(prof.MobilePhone))
                //{
                //    string factorId = factors.FirstOrDefault().Id;
                //    factorsClient.Reset(factorId);
                //}

                step = "Updating Secure Items";
                Task.Run(async () =>
                {
                     resp = await Factory.SecureItemUpdate(prof.Id, prof.SecureWord, prof.SecureImage
                        );
                }
                ).Wait();

                this.Master.ShowPopup($"Success! Profile successfuly updated");
            }
            catch (OktaException o)
            {
                string msg = string.Format("Error {0}: {1}", o.ErrorCode, o.ErrorSummary);
                this.Master.ShowPopup(msg);
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