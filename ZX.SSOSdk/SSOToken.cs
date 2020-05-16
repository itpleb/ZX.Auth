using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using ZX.SSOSdk.Utilities;
//using _Newtonsoft;
//using _Newtonsoft.Json;
namespace ZX.SSOSdk
{

    public interface ISSOToken
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
    public class SSOToken : ISSOToken
    {
        public SSOToken()
        {
        }
        public SSOToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return;
            }
            string v = Utilities.SSORSA.Decrypt(Utilities.SSOConst.PrivateKey, token);
            var obj = JsonHelper.DeserializeObject<SSOToken>(v);
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
            var str = Utilities.SSORSA.Encrypt(Utilities.SSOConst.PublicKey, v);
            //var sssssssss = Utilities.SSORSA.Decrypt(Utilities.SSOConst.PrivateKey, str);
            return str;
        }
         
    }



}
