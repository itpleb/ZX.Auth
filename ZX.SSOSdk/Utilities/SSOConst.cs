using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;

namespace ZX.SSOSdk.Utilities
{
    class SSOConst
    {
        private static string privateKey = "";

        private static string publicKey = "";
        public static string PublicKey
        {
            get
            {
                if (string.IsNullOrWhiteSpace(publicKey))
                {
                    publicKey = ReadFile("SSOPublickey.config");
                }
                return publicKey;

            }
        }
        public static string PrivateKey
        {
            get
            {
                if (string.IsNullOrWhiteSpace(privateKey))
                {
                    privateKey = ReadFile("SSOPrivatekey.config");
                }
                return privateKey;

            }
        }

        private static string ReadFile(string filename)
        {
            string fullname = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
            if (!File.Exists(fullname))
            {
                throw new System.Exception(string.Format("{0}_文件不存在", fullname));
            }
            return File.ReadAllText(fullname, Encoding.UTF8);
        }
    }
}
