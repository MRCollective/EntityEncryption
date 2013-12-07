using EntityEncryption.Processing;
using Xunit;

namespace EntityEncryption.Tests.Processing
{
    public class AESDataEncryptorTests
    {
        private const string TestKey = "e82GPy4Q333HWBGX1UOmKRKUxiaH/OViytNJxqWpWW8=";
        private const string TestIV = "Buj7srPbR73qjUTbXimNAg==";
        private readonly AESDataEncryptor _dataEncryptor;

        public AESDataEncryptorTests()
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

        [Fact]
        public void Return_same_string_on_decryption_if_not_base64()
        {
            const string testData = "54654ghhfhf";

            var decrypted = _dataEncryptor.Decrypt(testData, TestKey, TestIV);

            Assert.Equal(testData, decrypted);
        }

        [Fact]
        public void Decrypt_a_string()
        {
            const string testData = "PnUnj5urxpZYQebUSzyC8g==";

            var decrypted = _dataEncryptor.Decrypt(testData, TestKey, TestIV);

            Assert.Equal("testString", decrypted);
        }

        [Fact]
        public void Safely_decrypt_null_strings()
        {
            var decrypted = _dataEncryptor.Decrypt(null, TestKey, TestIV);

            Assert.Equal(null, decrypted);
        }
    }
}