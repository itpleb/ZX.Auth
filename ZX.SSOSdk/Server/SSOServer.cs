
using System;
using System.Collections.Generic;
using System.Web;
using ZX.SSOSdk.Server;

namespace ZX.SSOSdk.Server
{
    [Serializable]
    public class SSOServer : ISSOServer
    {
        //static readonly string CurrentUidSessionKey = "SSO_" + SSOHelper.GenerateMD5("#@./?<>#$@#$_" + typeof(SSOServer).FullName + "!@#!$#@%<>#$^^*&^&(*(");
        internal string ParameterPrefix { get; private set; } = "";
        internal SSOServer(string _ParameterPrefix)
        {
            ParameterPrefix = _ParameterPrefix;
        }
        public ISSOServerInfo GetRequestServer()
        {
            var siteid = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString[this.ParameterPrefix + "siteid"]);
            var strToken = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString[this.ParameterPrefix + "token"]);
            var returnurl = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString[this.ParameterPrefix + "returnurl"]);
            int.TryParse(HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString[this.ParameterPrefix + "action"]), out int action);
            var actionT = (SSOServerInfo.Action)action;
            var token = new SSOToken(strToken);
            var serverInfo = new SSOServerInfo(this)
            {
                SiteId = siteid,
                Token = token,
                ReturnUrl = returnurl,
                RequestAction = actionT,
            };
            return serverInfo;
        }

    }
}
