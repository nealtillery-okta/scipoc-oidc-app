using Forms45OIDCExample.TokenFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Forms45OIDCExample
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.EnsureAccess();
            string labelText;
            if (Request.IsAuthenticated)
            {
                var iden = System.Threading.Thread.CurrentPrincipal.Identity;
                labelText = $"You are authenticated. Hello, {iden.Name}";
            } else if ( Session["Tokens"] != null)
            {
                labelText = "You were authenticated using the Flow";

              
            }
            else
            {
                //should never get here.
                labelText = "You are not authenticated!";
            }

            var label = new Label
            {
                Text = labelText
            };

            var tok = (TokenModel)Session["Tokens"];
            
            var mainContent = (ContentPlaceHolder)Page.Form.FindControl("MainContent");

            mainContent.Controls.Add(label);

            if (tok != null)
            {
                System.Web.UI.HtmlControls.HtmlGenericControl accessBlock =
    new System.Web.UI.HtmlControls.HtmlGenericControl("pre");
                accessBlock.InnerText = $"ACCESS TOKEN: {tok.AccessToken.ToString()}";
                mainContent.Controls.Add(accessBlock);

                System.Web.UI.HtmlControls.HtmlGenericControl iDBlock =
    new System.Web.UI.HtmlControls.HtmlGenericControl("pre");
                iDBlock.InnerText = $"ID TOKEN: {tok.IDToken.ToString()}";
                mainContent.Controls.Add(iDBlock);


            }

        }
    }
}