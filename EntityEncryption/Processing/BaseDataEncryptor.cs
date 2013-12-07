using System;
using System.Diagnostics;
using System.Reflection;
using EntityEncryption.Attributes;
using EntityEncryption.Entities;
using Microsoft.SqlServer.Server;

namespace EntityEncryption.Processing
{
    public abstract class BaseDataEncryptor
    {
        public abstract string Encrypt(string data, string key, string iv);
        public abstract string Decrypt(string data, string key, string iv);

        public void EncryptProperties(IEncryptableEntity entity, string key, string iv)
        {
            ReplacePropertyState(entity, s => Encrypt(s, key, iv));
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