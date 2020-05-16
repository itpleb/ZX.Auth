using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZX.SSOSdk.Client
{
    [Serializable]
    public sealed class SSOClientConf
    {
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
    }
}
