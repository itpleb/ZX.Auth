
using System;
using System.Web;
using ZX.Auth.SSO.Interfaces;

namespace ZX.Auth.SSO.Server
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

            var siteid = HttpUtility.UrlDecode(SSOHttpContext.Current.Request.GetSSOQueryString()[this.ParameterPrefix + "siteid"]);
            var strToken = HttpUtility.UrlDecode(SSOHttpContext.Current.Request.GetSSOQueryString()[this.ParameterPrefix + "token"]);
            var returnurl = HttpUtility.UrlDecode(SSOHttpContext.Current.Request.GetSSOQueryString()[this.ParameterPrefix + "returnurl"]);
            int.TryParse(HttpUtility.UrlDecode(SSOHttpContext.Current.Request.GetSSOQueryString()[this.ParameterPrefix + "action"]), out int action);
            var actionT = (SSOServerInfo.Action)action;
            var token = new AuthToken(strToken);
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
