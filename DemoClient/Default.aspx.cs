using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZX.Auth;
using ZX.Auth.SSO.Client;
using ZX.Auth.SSO.Interfaces;

namespace DemoClient
{
    public partial class Default : System.Web.UI.Page
    {
        static readonly string SSOPageUrl = System.Configuration.ConfigurationManager.AppSettings["SSOPageUrl"];
        static readonly string Current_WebSite_Domain = System.Configuration.ConfigurationManager.AppSettings["Current_WebSite_Domain"];
        ISSOClient client = SSOHelper.CreateSSOClient("1", SSOPageUrl, "zx_");
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                string exInfo = "";
                var returnurl = Current_WebSite_Domain + this.Request.Url.PathAndQuery;
                if (!client.SignOn(returnurl))
                {

                    return;
                }
                this.Response.Write("欢迎[" + client.CurrentUid + "]");
            }
        }
        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            client.SignOut();
        }
    }
}