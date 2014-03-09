using System;
using System.Security.Cryptography;
using EntityEncryption.Processing;
using EntityEncryption.Tests.Entities;
using Xunit;
using Xunit.Extensions;

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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Throw_exception_if_attempting_to_encrypt_with_null_iv(string iv)
        {
            Assert.Throws<ArgumentNullException>(() => _dataEncryptor.Encrypt("Test", TestKey, iv));
        }

        [Fact]
        public void Apply_encryption_to_marked_properties()
        {
            var entity = new TestEntity
            {
                EncryptedData = "Test1",
                UnencryptedData = "Test2"
            };

            _dataEncryptor.EncryptProperties(entity, TestKey, TestIV);

            Assert.Equal("oqodVQZ34BVVvsOLVeMQBQ==", entity.EncryptedData);
            Assert.Equal("Test2", entity.UnencryptedData);
        }

        [Fact]
        public void Reuse_encryption_iv_if_present()
        {
            var entity = new TestEntity
            {
                EncryptedData = "Test1",
                Iv = "wp5EddOfzr/w9vtQz7mcEg=="
            };

            _dataEncryptor.EncryptProperties(entity, TestKey, TestIV);

            Assert.Equal("6zreAe6YS/JMbI1zw7NF5g==", entity.EncryptedData);
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

        [Fact]
        public void Apply_decryption_to_marked_properties()
        {
            var entity = new TestEntity
            {
                EncryptedData = "oqodVQZ34BVVvsOLVeMQBQ==",
                UnencryptedData = "Test2",
                Iv = TestIV
            };

            _dataEncryptor.DecryptProperties(entity, TestKey);

            Assert.Equal("Test1", entity.EncryptedData);
            Assert.Equal("Test2", entity.UnencryptedData);
        }

        [Fact]
        public void Avoid_decryption_if_no_iv_present()
        {
            var entity = new TestEntity
            {
                EncryptedData = "oqodVQZ34BVVvsOLVeMQBQ==",
                UnencryptedData = "Test2"
            };

            _dataEncryptor.DecryptProperties(entity, TestKey);

            Assert.Equal("oqodVQZ34BVVvsOLVeMQBQ==", entity.EncryptedData);
            Assert.Equal("Test2", entity.UnencryptedData);
        }
    }
}