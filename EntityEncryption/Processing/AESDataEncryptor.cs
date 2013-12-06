using System;
using System.IO;
using System.Security.Cryptography;

namespace EntityEncryption.Processing
{
    public class AESDataEncryptor : IDataEncryptor
    {
        public string Encrypt(string data, string key, string iv)
        {
            if (string.IsNullOrEmpty(data))
                return data;

            using (var serviceProvider = new AesCryptoServiceProvider())
            {
                using (var encryptor = serviceProvider.CreateEncryptor(Convert.FromBase64String(key), Convert.FromBase64String(iv)))
                using (var m = new MemoryStream())
                using (var c = new CryptoStream(m, encryptor, CryptoStreamMode.Write))
                {
                    using (var w = new StreamWriter(c))
                    {
                        w.Write(data);
                    }
                    return Convert.ToBase64String(m.ToArray());
                }
            }
        }
    }
}