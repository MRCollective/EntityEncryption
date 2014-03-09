using System;
using System.Security.Cryptography;
using EntityEncryption.Base.IVGenerators;
using Xunit;

namespace EntityEncryption.Base.Tests.IVGenerators
{
    public class AESIVGeneratorTests
    {
        [Fact]
        public void Generate_valid_new_iv()
        {
            var ivGenerator = new AESIVGenerator();

            var iv = ivGenerator.NewIV();

            using (var serviceProvider = new AesCryptoServiceProvider())
            {
                Assert.DoesNotThrow(() => serviceProvider.IV = Convert.FromBase64String(iv));
            }
        }
    }
}