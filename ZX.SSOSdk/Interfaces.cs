using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SSOServerLibrary
{

    public interface ISSOServer: ZX.SSOSdk.Server.Interfaces.ISSOServerInfo
    {
        //string SiteId { get; set; }
        //SSOToken Token { get; set; }
        //string ReturnUrl { get; set; }
    }

    //public interface ISSOController
    //{
    //    ISSOServer GetRequestSSOServer();
    //    string GetResponseToken();
    //    int CheckUser(string uid, string password, string siteid);
    //    bool CheckUser();
    //    void Redirect(string returnUrl);
    //    void SignOut();
    //    void SignOn(string uid, string siteid, string returnUrl);
    //}

    //public interface ISSOUser
    //{
    //    string Uid { get; set; }
    //    string Password { get; set; }
    //}
}
