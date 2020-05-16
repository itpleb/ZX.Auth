
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ZX.SSOSdk.Utilities
{
    internal sealed class SSORSA
    {
        static string ToHex(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in bytes)
            {
                sb.Append(item.ToString("X2"));
            }
            return sb.ToString();
        }
        static byte[] FromHex(string hexString)
        {
            if (hexString.Length % 2 != 0)//编码为16进制,必须为2的倍数。
            {
                throw new System.Exception("编码格式不正确");
            }
            List<byte> result = new List<byte>();
            for (int i = 0; i < hexString.Length; i += 2) //每两位为一个字节
            {
                string lowCode = hexString.Substring(i, 2); //取出低字节,并以16进制进制转换
                result.Add(Convert.ToByte(lowCode, 16));
            }
            return result.ToArray();

        }

        internal static string Encrypt(string publicKey, string rawInput)
        {
            if (string.IsNullOrEmpty(rawInput))
            {
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(publicKey))
            {
                throw new ArgumentException("Invalid Public Key");
            }

            using (var rsaProvider = new RSACryptoServiceProvider())
            {
                var inputBytes = Encoding.UTF8.GetBytes(rawInput);//有含义的字符串转化为字节流
                rsaProvider.FromXmlString(publicKey);//载入公钥
                int bufferSize = (rsaProvider.KeySize / 8) - 11;//单块最大长度
                var buffer = new byte[bufferSize];
                using (MemoryStream inputStream = new MemoryStream(inputBytes),
                     outputStream = new MemoryStream())
                {
                    while (true)
                    { //分段加密
                        int readSize = inputStream.Read(buffer, 0, bufferSize);
                        if (readSize <= 0)
                        {
                            break;
                        }

                        var temp = new byte[readSize];
                        Array.Copy(buffer, 0, temp, 0, readSize);
                        var encryptedBytes = rsaProvider.Encrypt(temp, false);
                        outputStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                    }
                    return Convert.ToBase64String(outputStream.ToArray());//转化为字节流方便传输
                }
            }
        }

        internal static string Decrypt(string privateKey, string encryptedInput)
        {
            if (string.IsNullOrEmpty(encryptedInput))
            {
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(privateKey))
            {
                throw new ArgumentException("Invalid Private Key");
            }

            using (var rsaProvider = new RSACryptoServiceProvider())
            {
                var inputBytes = Convert.FromBase64String(encryptedInput);
                rsaProvider.FromXmlString(privateKey);
                int bufferSize = rsaProvider.KeySize / 8;
                var buffer = new byte[bufferSize];
                using (MemoryStream inputStream = new MemoryStream(inputBytes),
                     outputStream = new MemoryStream())
                {
                    while (true)
                    {
                        int readSize = inputStream.Read(buffer, 0, bufferSize);
                        if (readSize <= 0)
                        {
                            break;
                        }

                        var temp = new byte[readSize];
                        Array.Copy(buffer, 0, temp, 0, readSize);
                        var rawBytes = rsaProvider.Decrypt(temp, false);
                        outputStream.Write(rawBytes, 0, rawBytes.Length);
                    }
                    return Encoding.UTF8.GetString(outputStream.ToArray());
                }
            }
        }


        internal static void CreateKey(int dwKeySize, out string privateKey, out string publicKey)
        {
            privateKey = "";
            publicKey = "";
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(dwKeySize);
            privateKey = rsa.ToXmlString(true);
            publicKey = rsa.ToXmlString(false);
        }

        internal static string GenerateMD5(string txt)
        {
            using (var mi = System.Security.Cryptography.MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(txt);
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

    }
}













































































