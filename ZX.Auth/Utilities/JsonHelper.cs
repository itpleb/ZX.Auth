using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;

namespace ZX.Auth.Utilities
{
    public static class JsonHelper
    {
        public static string SerializeObject(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            //string result = String.Empty;
            //DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            //using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            //{
            //    serializer.WriteObject(ms, obj);
            //    result = encoding.GetString(ms.ToArray());
            //}
            //return result;
            var serializer = new DataContractJsonSerializer(obj.GetType())
            {

            };
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                var result = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                return result;
            }
            //return _Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        public static T DeserializeObject<T>(string json)
        {
            T resultObject = Activator.CreateInstance<T>();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(json)))
            {
                resultObject = (T)serializer.ReadObject(ms);
            }
            return resultObject;
        }
    }
}
