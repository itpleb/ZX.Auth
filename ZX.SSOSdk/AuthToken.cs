using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using ZX.SSOSdk.Utilities;

namespace ZX.SSOSdk
{
    public interface IAuthToken : ISSOToken
    {

    }
    [Serializable]
    [DataContract]
    public class AuthToken : SSOToken, IAuthToken
    {
        public AuthToken()
        {
        }
        public AuthToken(string token) : base(token)
        {
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(uid))
            {
                return null;
            }
            var v = this.SerializeObject();
            var str = Utilities.SSORSA.Encrypt(Utilities.SSOConst.PublicKey, v);
            //var sssssssss = Utilities.SSORSA.Decrypt(Utilities.SSOConst.PrivateKey, str);
            return str;
        }
    }
}
