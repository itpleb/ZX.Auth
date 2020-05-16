using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZX.SSOSdk.Server
{
    public interface ISSOSite
    {
        string SiteId { get; }
        string HomePage { get; }
    }
}
