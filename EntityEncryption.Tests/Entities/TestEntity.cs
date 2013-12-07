using EntityEncryption.Attributes;
using EntityEncryption.Entities;

namespace EntityEncryption.Tests.Entities
{
    public class TestEntity : IEncryptableEntity
    {
        public string Iv { get; set; }
        [Encrypt]
        public string EncryptedData { get; set; }
        public string UnencryptedData { get; set; }
    }
}