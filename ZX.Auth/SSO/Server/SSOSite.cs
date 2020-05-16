using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZX.Auth.SSO.Interfaces;

namespace ZX.Auth.SSO.Server
{
    [Serializable]
    public class SSOSite : ISSOSite
    {
        public SSOSite()
        {

        }
        public string SiteId { get; internal set; }
        public string HomePage { get; internal set; }
    }
}
