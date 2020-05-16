using System;
using ZX.Auth.SSO.Interfaces;
using ZX.Auth.Utilities;

namespace ZX.Auth.SSO.Server
{
    [Serializable]
    public class SSOServerInfo : ISSOServerInfo
    {
        SSOServer ResponseServer { get; set; }
        internal SSOServerInfo(SSOServer responseServer)
        {
            ResponseServer = responseServer;
        }


        public string SiteId { get; internal set; } = "";
        public IAuthToken Token { get; internal set; } = new AuthToken();
        public string ReturnUrl { get; internal set; } = "";
        public Action RequestAction { get; internal set; } = Action.SignOn;

        public ISSOSite GetSiteInfo()
        {
            return SSOSites.Instance.GetSite(this.SiteId);
        }


        #region ResponseServer


        IAuthToken GetResponseToken(string uid4Token, double tmeout = 60 * 60 * 24)
        {
            var token = new AuthToken()
            {
                uid = uid4Token,
                timeout = tmeout
            };
            return token;
        }

        public IAuthToken GetResponseToken(string uid4Token, TimeSpan timeSpan)
        {
            return GetResponseToken(uid4Token, timeSpan.TotalSeconds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid4Token"></param>
        /// <param name="tokenTimeout"></param>
        public void SignOn(string uid4Token, int tmeout = 60 * 60 * 24)
        {
            //1.保存用户id
            //2.跳转Url+token
            if (!string.IsNullOrEmpty(uid4Token))
            {
                var token = GetResponseToken(uid4Token, tmeout);
                var tokenStr = token.ToString();
                var new_returnUrl = UrlHelper.GetUrl(this.ReturnUrl, this.ResponseServer.ParameterPrefix + "sso_token", tokenStr);
                SSOHttpContext.Current.Response.Redirect(new_returnUrl);
            }
        }

        //var token = GetResponseToken(uid4Token, TimeSpan.FromDays(1));

        #endregion
        public enum Action
        {
            SignOn = 0,
            SignOut = 1,
            CheckSSOState = 2
        }
    }
}
