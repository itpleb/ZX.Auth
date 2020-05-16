using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
#if NETX

#else
using System.Net.Http;
using Microsoft.AspNetCore.Http;
#endif
using System.Text;
using System.Web;
using ZX.Auth.Utilities;
using ZX.Auth;
using ZX.Auth.SSO.Interfaces;

namespace ZX.Auth.SSO.Client
{
    [Serializable]
    public class SSOClient : ISSOClient
    {
        //static readonly string CurrentTokenSessionKey = "SSO_" + SSOHelper.GenerateMD5("!@#!$#@%<>#$^^*&^&(*(" + typeof(SSOClient).FullName + "#@./?<>#$@#$_");

        /// <summary>
        /// 当前站点ID
        /// </summary>
        public string SiteId { get; set; } = "";
        /// <summary>
        /// SSO认证页面
        /// </summary>
        public string SSOPageUrl { get; set; } = "";
        /// <summary>
        /// 参数前缀
        /// </summary>
        public string ParameterPrefix { get; set; } = "";


        internal SSOClient(string _SiteId, string _SSOPageUrl, string _ParameterPrefix)
        {
            SiteId = _SiteId;
            SSOPageUrl = _SSOPageUrl;
            ParameterPrefix = _ParameterPrefix;
        }

        //internal SSOClient(SSOClientConf conf)
        //{
        //    SiteId = _SiteId;
        //    SSOPageUrl = _SSOPageUrl;
        //}

        private IAuthToken GetRequestToken(out bool requestByAjax, out string bt_sso_returnurl)
        {

            requestByAjax = false;
            bt_sso_returnurl = "";
            var tokenStr = "";
            if (SSOHttpContext.Current.Request.Headers[this.ParameterPrefix + "sso_ajax"] == "1")
            {
                requestByAjax = true;
                bt_sso_returnurl = SSOHttpContext.Current.Request.Headers[this.ParameterPrefix + "sso_returnurl"];
                tokenStr = SSOHttpContext.Current.Request.Headers[this.ParameterPrefix + "sso_token"];
                var _token = new AuthToken(tokenStr);
                return _token;
            }
            else
            {
                tokenStr = SSOHttpContext.Current.Request.GetSSOQueryString()[this.ParameterPrefix + "sso_token"];
                if (!string.IsNullOrWhiteSpace(tokenStr))
                {
                    var _token = new AuthToken(tokenStr);
                    if (_token.IsValid)
                    {
                        return _token; ;
                    }
                }
                tokenStr = SSOHttpContext.Current.Request.GetSSOQueryString()["token"];
                var token = new AuthToken(tokenStr);
                return token;
            }
        }
        public IAuthToken GetRequestToken()
        {
            var requestByAjax = false;
            var bt_sso_returnurl = "";
            return GetRequestToken(out requestByAjax, out bt_sso_returnurl);
        }


        [Obsolete("过期，请使用方法GetRequestToken()", true)]
        public IAuthToken CurrentToken
        {
            get
            {
                return GetRequestToken();
            }
        }

        public string CurrentUid
        {
            get
            {
                var token = GetRequestToken();
                if (token != null && token.IsValid)
                {
                    return token.uid;
                }
                return "";
            }
        }

        public bool SignOn(string returnurl)
        {
            var requestByAjax = false;
            var bt_sso_returnurl = "";
            var token = GetRequestToken(out requestByAjax, out bt_sso_returnurl);
            if (!token.IsValid)
            {
                returnurl = requestByAjax ? bt_sso_returnurl : returnurl;

                var url = this.SSOPageUrl + "?"
                    + this.ParameterPrefix + "action=" + ((int)Server.SSOServerInfo.Action.SignOn) + "&"
                    + this.ParameterPrefix + "siteid=" + HttpUtility.UrlEncode(this.SiteId) + "&"
                    + this.ParameterPrefix + "returnurl=" + HttpUtility.UrlEncode(returnurl);
                if (!requestByAjax)
                {
                    SSOHttpContext.Current.Response.Redirect(url);
                }
                else
                {
                    var result = new
                    {
                        status_code = 401,
                        sso_url = url,
                    };
                    SSOHttpContext.Current.Response.Clear();
                    SSOHttpContext.Current.Response.ContentType = "application/json";
                    var json = result.SerializeObject();
#if NETX
                SSOHttpContext.Current.Response.Write(json);
#else
                    var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                    SSOHttpContext.Current.Response.Body.Write(buffer);
#endif

                }
                return false;
            }
            else
            {
                return true;
            }
        }
        public void SignOut()
        {

        }
    }
}
