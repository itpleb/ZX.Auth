using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace ZX.Auth.Utilities
{
    class AuthKeys
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
