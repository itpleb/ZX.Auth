
using System;
using System.Web;
using ZX.Auth.SSO.Client;
using ZX.Auth.SSO.Interfaces;
using ZX.Auth.SSO.Server;
using ZX.Auth.Utilities;

#if NETX

#else
using Microsoft.AspNetCore.Http;
#endif

namespace ZX.Auth
{
    public static class SSOHelper
    {

        /// <summary>
        /// 同一个SSOServer只创建一次
        /// </summary>
        /// <returns></returns>
        public static ISSOServer CreateSSOServer(string _ParameterPrefix = "")
        {

            return new SSOServer(_ParameterPrefix)
            {

            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_SiteId"></param>
        /// <param name="_SSOPageUrl"></param>
        /// <param name="_ParameterPrefix">要和SSO服务端的前一致</param>
        /// <returns></returns>
        public static ISSOClient CreateSSOClient(string _SiteId, string _SSOPageUrl, string _ParameterPrefix = "")
        {

            return new SSOClient(_SiteId, _SSOPageUrl, _ParameterPrefix)
            {

            };
        }


        public static void CreateRsaKey(int dwKeySize, out string privateKey, out string publicKey)
        {
            CryptoService.CreateKey(dwKeySize, out privateKey, out publicKey);
        }


        public static IAuthToken CreateToken(string uid4Token, double tmeout = 60 * 60 * 24)
        {
            var token = new AuthToken()
            {
                uid = uid4Token,
                timeout = tmeout
            };
            return token;
        }

        public static IAuthToken CreateToken(string uid4Token, TimeSpan timeSpan)
        {
            return CreateToken(uid4Token, timeSpan.TotalSeconds);
        }


        public static IAuthToken GetToken(string json)
        {
            var _token = new AuthToken(json);
            return _token;
        }


#if NETX

#else
        private static IHttpContextAccessor _accessor;

        public static Microsoft.AspNetCore.Http.HttpContext Current => _accessor.HttpContext;

        public static void Configure(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
#endif




        internal static
#if NETX
            System.Collections.Specialized.NameValueCollection 
#else
            IQueryCollection
#endif
            GetSSOQueryString(this HttpRequest ht)
        {

#if NETX
            return SSOHttpContext.Current.Request.QueryString;
#else
            return SSOHelper.Current.Request.Query;
#endif
        }
    }

    class SSOHttpContext
    {
        internal static readonly SSOHttpContext Current = new SSOHttpContext();
        internal HttpRequest Request
        {
            get
            {
#if NETX
                return HttpContext.Current.Request;
#else
                return SSOHelper.Current.Request;
#endif

            }
        }


        internal HttpResponse Response
        {
            get
            {
#if NETX
                return HttpContext.Current.Response;
#else
                return SSOHelper.Current.Response;
#endif

            }
        }










    }
}