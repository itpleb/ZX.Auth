using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using ZX.Auth.SSO.Server;
using ZX.Auth.Utilities;
namespace DemoServer.SSO
{
    /// <summary>
    /// sso 的摘要说明
    /// </summary>
    public class sso : IHttpHandler, IRequiresSessionState
    {
        internal static readonly ZX.Auth.SSO.Interfaces.ISSOServer btSSOServer = ZX.Auth.SSOHelper.CreateSSOServer("zx_");
        const string SignOnPageUrl = "/index.asp";
        const string SSOPageUrl = "/SSO/sso.ashx";
        public bool IsReusable { get { return true; } }

        /// <summary>
        /// 在这里判断是否在服务端登录了
        /// </summary>
        internal static string CurrentLoginUserID
        {
            get
            {
                try
                {
                    var uid = Convert.ToString(HttpContext.Current.Session["uuid"]);
                    return uid;
                }
                catch (Exception ex)
                {

                }
                return "";
            }
            set
            {
                HttpContext.Current.Session["uuid"] = value;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            var requestServer = btSSOServer.GetRequestServer();
            if (string.IsNullOrEmpty(requestServer.SiteId))
            {
                context.Response.ContentType = "application/json";
                var result = new
                {
                    ret = false,
                    err_msg = "请确保SiteID参数是否正确",
                };
                var json = result.SerializeObject();
                context.Response.Write(json);
                return;
            }
            if (requestServer.RequestAction == SSOServerInfo.Action.SignOut)
            {
                //_Newtonsoft.Json.Converters.
            }
            else if (requestServer.RequestAction == SSOServerInfo.Action.CheckSSOState)//
            {
                context.Response.ContentType = "application/json";
                var uid4Token = Convert.ToString(CurrentLoginUserID);
                if (string.IsNullOrWhiteSpace(CurrentLoginUserID) || requestServer.Token.uid != uid4Token)
                {
                    var result = new
                    {
                        ret = false,
                        err_msg = "用户未登录",
                    };
                    var json = result.SerializeObject();
                    context.Response.Write(json);
                    return;
                }
                else
                {

                    var token = requestServer.GetResponseToken(uid4Token, TimeSpan.FromDays(1));
                    var result = new
                    {
                        ret = true,
                        token = token,
                    };
                    var json = result.SerializeObject();
                    context.Response.Write(json);
                    return;
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(CurrentLoginUserID))
                {
                    var uid4Token = Convert.ToString(CurrentLoginUserID);
                    requestServer.SignOn(uid4Token);//已经登录过了，直接跳回原来的页面
                }
                else//还没有登录的直接跳到登录页面
                {
                    var sso_url = context.Request.Url.PathAndQuery;
                    var url = SignOnPageUrl + "?zx_login_type=sso&sso_url=" + HttpContext.Current.Server.UrlEncode(sso_url);
                    context.Response.Redirect(url);
                }
            }

        }

    }
}