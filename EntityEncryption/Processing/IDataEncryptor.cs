using EntityEncryption.Entities;

namespace EntityEncryption.Processing
{
    public interface IDataEncryptor
    {
        string Encrypt(string data, string key, string iv);
        string Decrypt(string data, string key, string iv);
        void EncryptProperties(IEncryptableEntity entity, string key, string iv);
        void DecryptProperties(IEncryptableEntity entity, string key);
    }
}