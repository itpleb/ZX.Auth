using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZX.SSOSdk.Client
{
    public interface ISSOClient
    {
        string SiteId { get; }
        string SSOPageUrl { get; }

        ISSOToken GetRequestToken();

        ISSOToken CurrentToken { get; }
        string CurrentUid { get; }


        bool SignOn(string returnurl);
        void SignOut();
    }

}
