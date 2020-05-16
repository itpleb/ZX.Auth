using DemoServer.SSO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoServer
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_LoggingIn(object sender, LoginCancelEventArgs e)
        {
            sso.CurrentLoginUserID = Login1.UserName;
            var sso_url = this.Request.QueryString["sso_url"];

            if (!string.IsNullOrWhiteSpace(sso_url))
            {
                this.Response.Redirect(sso_url);
            }


        }
    }
}