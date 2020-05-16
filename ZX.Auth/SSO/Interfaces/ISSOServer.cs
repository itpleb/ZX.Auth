using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZX.Auth.SSO.Interfaces
{
    public interface ISSOServer
    {
        ISSOServerInfo GetRequestServer();
    }
}
