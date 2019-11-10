using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Caching;
using Okta.Core.Models;
using Okta.Core.Clients;

namespace Forms45OIDCExample.TokenFlow.authorization_code
{
    public partial class callback : Page
    {
        protected TokenModel _tokens = new TokenModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.AllKeys.Contains("code"))
            {
                var code = Request.QueryString["code"];
                var provider = new TokenFlow.Provider();

                Task.Run(async () => { _tokens = await provider.ObtainTokens(code); }).Wait();
                Session["Tokens"] = _tokens;

                string userId = _tokens.IDToken.Subject;
                User user = Factory.UserClient.Get(userId);
                UserFactorsClient factorsClient = Factory.UserClient.GetUserFactorsClient(user);
                IEnumerable<Factor> factors = factorsClient.Where(f => f.FactorType == FactorType.Sms);
                bool hasSmsFactor = factors.Count() == 1;

                if (!hasSmsFactor && !string.IsNullOrEmpty(user.Profile.MobilePhone))
                {
                    // send the user to the SMS enrollment page if they aren't enrolled
                    // and their profile has a phone number
                    Session["DestinationUrl"] = "/EnrollFactor.aspx";
                }

                if (Session["DestinationUrl"] != null)
                {
                    var uri = Session["DestinationUrl"];
                    Session["DestinationUrl"] = null;
                    Response.Redirect(uri.ToString());
                }
                else
                {
                    Response.Redirect("~/");
                }
            }
        }
    }
}