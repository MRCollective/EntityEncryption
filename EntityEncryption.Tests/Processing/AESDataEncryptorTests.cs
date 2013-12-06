using EntityEncryption.Processing;
using Xunit;

namespace EntityEncryption.Tests.Processing
{
    public class AESEncryptorTests
    {
        private const string TestKey = "e82GPy4Q333HWBGX1UOmKRKUxiaH/OViytNJxqWpWW8=";
        private const string TestIV = "Buj7srPbR73qjUTbXimNAg==";
        private readonly AESDataEncryptor _dataEncryptor;

        public AESEncryptorTests()
        {
            _dataEncryptor = new AESDataEncryptor();
        }

        [Fact]
        public void Encrypt_a_string()
        {
            const string testData = "testString";

            var encrypted = _dataEncryptor.Encrypt(testData, TestKey, TestIV);

            Assert.Equal("PnUnj5urxpZYQebUSzyC8g==", encrypted);
        }

        [Fact]
        public void Leave_null_strings_untouched_after_encryption()
        {
            var encrypted = _dataEncryptor.Encrypt(null, TestKey, TestIV);

            Assert.Equal(null, encrypted);
        }
    }
}