using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZX.Auth.SSO.Server;

namespace ZX.Auth.SSO.Interfaces
{
    public interface ISSOServerInfo
    {
        string SiteId { get; }
        IAuthToken Token { get; }
        string ReturnUrl { get; }
        SSOServerInfo.Action RequestAction { get; }


        ISSOSite GetSiteInfo();
        void SignOn(string uid, int timeout = 60 * 60 * 24);

        //ISSOToken GetResponseToken(string uid4Token, double timeout = 60 * 60 * 24);

        IAuthToken GetResponseToken(string uid4Token, TimeSpan timeSpan);

    }
}
