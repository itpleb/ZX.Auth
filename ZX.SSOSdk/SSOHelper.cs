using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Web;
using ZX.SSOSdk.Client;
using ZX.SSOSdk.Utilities;

namespace ZX.SSOSdk
{
    public static class SSOHelper
    {

        /// <summary>
        /// 同一个SSOServer只创建一次
        /// </summary>
        /// <returns></returns>
        public static ZX.SSOSdk.Server.ISSOServer CreateSSOServer(string _ParameterPrefix = "")
        {

            return new ZX.SSOSdk.Server.SSOServer(_ParameterPrefix)
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
        public static Client.ISSOClient CreateSSOClient(string _SiteId, string _SSOPageUrl, string _ParameterPrefix = "")
        {

            return new Client.SSOClient(_SiteId, _SSOPageUrl, _ParameterPrefix)
            {

            };
        }


        public static void CreateRsaKey(int dwKeySize, out string privateKey, out string publicKey)
        {
            SSORSA.CreateKey(dwKeySize, out privateKey, out publicKey);
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



     



    }
}