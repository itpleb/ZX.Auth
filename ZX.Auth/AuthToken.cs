using System;
using System.Runtime.Serialization;
using ZX.Auth.Utilities;

namespace ZX.Auth
{
    public interface IAuthToken
    {
        /// <summary>
        /// token id-比如用户ID等
        /// </summary>
        string uid { get; }
        /// <summary>
        /// token有效时长（单位秒）
        /// </summary>
        double timeout { get; }
        //IDictionary<string, object> parms { get; }
        string ToString();

        bool IsValid { get; }
    }
    [Serializable]
    [DataContract]
    public sealed class AuthToken : IAuthToken 
    {
        public AuthToken()
        {
        }
        public AuthToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return;
            }
            string v = CryptoService.Decrypt(AuthKeys.PrivateKey, token);
            var obj = JsonHelper.DeserializeObject<AuthToken>(v);
            this.uid = obj.uid;
            this.timeout = obj.timeout;
            this.timestamp = obj.timestamp;
            //this.parms = obj.parms;
        }


        /// <summary>
        /// token id-比如用户ID等
        /// </summary>
        //[JsonProperty("uid")]
        [DataMember(Name = "uid")]
        public string uid { get; internal set; }
        /// <summary>
        /// token有效时长（单位秒）
        /// </summary>
        //[JsonProperty("timeout")]
        [DataMember(Name = "timeout")]
        public double timeout { get; internal set; }

        /// <summary>
        /// token生成的时间（Utc）
        /// </summary>
        //[JsonProperty("ts")]
        [DataMember(Name = "ts")]
        public long timestamp { get; internal set; } = DateTime.UtcNow.ToFileTimeUtc();


        //[JsonProperty("parms")]
        //public IDictionary<string, object> parms { get; internal set; } = new Dictionary<string, object>();

        //[JsonIgnore]
        public bool IsValid
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.uid) || Math.Abs((DateTime.UtcNow - DateTime.FromFileTimeUtc(timestamp)).TotalSeconds) > timeout)
                {
                    return true;
                }
                return false;
            }
        }


        //public void AddParms(string key, object value)
        //{
        //    parms.Add(key.ToLower().Trim(), value);
        //}
        //public void ClearParms()
        //{
        //    parms.Clear();
        //}

        public override string ToString()
        {
            if (string.IsNullOrEmpty(uid))
            {
                return null;
            }
            var v = this.SerializeObject();
            var str = CryptoService.Encrypt(AuthKeys.PublicKey, v);
            return str;
        }

    }

}
