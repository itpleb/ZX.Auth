using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace ZX.Auth.SSO.Client
{
    public interface ISSOClient
    {
        string SiteId { get; }
        string SSOPageUrl { get; }

        IAuthToken GetRequestToken();

        IAuthToken CurrentToken { get; }

        string CurrentUid { get; }


        bool SignOn(string returnurl);
        void SignOut();
    }

}
