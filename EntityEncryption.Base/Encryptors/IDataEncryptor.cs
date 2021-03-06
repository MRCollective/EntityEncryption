﻿using EntityEncryption.Base.Entities;

namespace EntityEncryption.Base.Encryptors
{
    public interface IDataEncryptor
    {
        string Encrypt(string data, string key, string iv);
        string Decrypt(string data, string key, string iv);
        void EncryptProperties(IEncryptableEntity entity, string key);
        void DecryptProperties(IEncryptableEntity entity, string key);
    }
}