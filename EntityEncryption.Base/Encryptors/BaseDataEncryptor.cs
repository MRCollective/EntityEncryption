using System;
using System.Reflection;
using EntityEncryption.Base.Attributes;
using EntityEncryption.Base.Entities;
using EntityEncryption.Base.IVGenerators;

namespace EntityEncryption.Base.Encryptors
{
    public abstract class BaseDataEncryptor
    {
        public abstract string Encrypt(string data, string key, string iv);
        public abstract string Decrypt(string data, string key, string iv);
        
        private readonly IIVGenerator _ivGenerator;

        protected BaseDataEncryptor(IIVGenerator ivGenerator)
        {
            _ivGenerator = ivGenerator;
        }

        public void EncryptProperties(IEncryptableEntity entity, string key)
        {
            var iv = entity.Iv ?? _ivGenerator.NewIV();

            ReplacePropertyState(entity, s => Encrypt(s, key, iv));
            entity.Iv = iv;
        }

        public void DecryptProperties(IEncryptableEntity entity, string key)
        {
            ReplacePropertyState(entity, s => Decrypt(s, key, entity.Iv));
        }

        private static void ReplacePropertyState(IEncryptableEntity entity, Func<string, string> replacementFunction)
        {
            var properties = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var propertyInfo in properties)
            {
                var attr = propertyInfo.GetCustomAttributes(typeof(EncryptAttribute), false);
                var property = entity.GetType().GetProperty(propertyInfo.Name);
                var originalValue = property.GetValue(entity, null);
                if (attr.Length > 0)
                {
                    property.SetValue(entity, replacementFunction(originalValue as string), null);
                }
            }
        }
    }
}