using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZX.SSOSdk.Server
{
    public interface ISSOServerState
    {
        void Add(string uid, string siteid);
        List<string> GetLoginSites(string uid);
        System.Collections.ICollection GetUserCollection();
        int GetUserCount();
        void RemoveUser(string uid);
    }

}
