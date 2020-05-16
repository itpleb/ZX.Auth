using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZX.SSOSdk.Server
{
    public interface ISSOServerInfo
    {
        string SiteId { get; }
        ISSOToken Token { get; }
        string ReturnUrl { get; }
        SSOServerInfo.Action RequestAction { get; }


        ISSOSite GetSiteInfo();
        void SignOn(string uid, int timeout = 60 * 60 * 24);

        //ISSOToken GetResponseToken(string uid4Token, double timeout = 60 * 60 * 24);

        ISSOToken GetResponseToken(string uid4Token, TimeSpan timeSpan);

    }
}
