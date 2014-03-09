using EntityEncryption.Base.Attributes;
using EntityEncryption.Base.Entities;

namespace EntityEncryption.Base.Tests.Entities
{
    public class TestEntity : IEncryptableEntity
    {
        public string Iv { get; set; }
        [Encrypt]
        public string EncryptedData { get; set; }
        public string UnencryptedData { get; set; }
    }
}