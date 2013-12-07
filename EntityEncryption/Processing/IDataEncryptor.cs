namespace EntityEncryption.Processing
{
    public interface IDataEncryptor
    {
        string Encrypt(string data, string key, string iv);
        string Decrypt(string data, string key, string iv);
    }
}