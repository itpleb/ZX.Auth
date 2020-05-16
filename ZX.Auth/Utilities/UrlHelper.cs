using System;
using System.Collections.Generic;
using System.Web;

namespace ZX.Auth.Utilities
{
    class UrlHelper
    {
        internal static string GetUrl(string url, Dictionary<string, string> keyvalues)
        { 
            List<string> list = new List<string>();
            foreach (var item in keyvalues)
            {
                list.Add(string.Format("{0}={1}", item.Key, HttpUtility.UrlEncode(item.Value)));
            }
            string param = String.Join("&", list.ToArray());
            Uri uri = new Uri(url);
            string op = string.IsNullOrEmpty(uri.Query) ? "?" : "&";
            return String.Concat(url, op, param);
        }
        internal static string GetUrl(string url, string paramName, string paramValue)
        {
            Uri uri = new Uri(url);
            string op = string.IsNullOrEmpty(uri.Query) ? "?" : "&";
            string param = string.Format("{0}={1}", paramName, HttpUtility.UrlEncode(paramValue));
            return String.Concat(url, op, param);
        }
    }
}
