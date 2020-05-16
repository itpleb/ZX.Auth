using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ZX.SSOSdk.Utilities;
namespace ZX.SSOSdk.Client
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

        private ISSOToken GetRequestToken(out bool requestByAjax, out string bt_sso_returnurl)
        {
            requestByAjax = false;
            bt_sso_returnurl = "";
            var tokenStr = "";
            if (HttpContext.Current.Request.Headers[this.ParameterPrefix + "sso_ajax"] == "1")
            {
                requestByAjax = true;
                bt_sso_returnurl = HttpContext.Current.Request.Headers[this.ParameterPrefix + "sso_returnurl"];
                tokenStr = HttpContext.Current.Request.Headers[this.ParameterPrefix + "sso_token"];
                var _token = new SSOToken(tokenStr);
                return _token;
            }
            else
            {
                tokenStr = System.Web.HttpContext.Current.Request.QueryString[this.ParameterPrefix + "sso_token"];
                if (!string.IsNullOrWhiteSpace(tokenStr))
                {
                    var _token = new SSOToken(tokenStr);
                    if (_token.IsValid)
                    {
                        return _token; ;
                    }
                }
                tokenStr = System.Web.HttpContext.Current.Request.QueryString["token"];
                var token = new SSOToken(tokenStr);
                return token;
            }
        }
        public ISSOToken GetRequestToken()
        {
            var requestByAjax = false;
            var bt_sso_returnurl = "";
            return GetRequestToken(out requestByAjax, out bt_sso_returnurl);
        }



        [Obsolete("过期，请使用方法GetRequestToken()")]
        public ISSOToken CurrentToken
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
                    + this.ParameterPrefix + "siteid=" + HttpContext.Current.Server.UrlEncode(this.SiteId) + "&"
                    + this.ParameterPrefix + "returnurl=" + HttpContext.Current.Server.UrlEncode(returnurl);
                if (!requestByAjax)
                {
                    HttpContext.Current.Response.Redirect(url);
                }
                else
                {
                    var result = new
                    {
                        status_code = 401,
                        sso_url = url,
                    };
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ContentType = "application/json";
                    var json = result.SerializeObject();
                    HttpContext.Current.Response.Write(json);
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
