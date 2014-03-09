using System;
using System.Security.Cryptography;

namespace EntityEncryption.Base.IVGenerators
{
    public class AESIVGenerator : IIVGenerator
    {
        public string NewIV()
        {
            string iv;
            using (var serviceProvider = new AesCryptoServiceProvider())
            {
                serviceProvider.GenerateIV();
                iv = Convert.ToBase64String(serviceProvider.IV);
            }
            return iv;
        }
    }
}
