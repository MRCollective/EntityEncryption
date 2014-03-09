using EntityEncryption.Base.Encryptors;
using EntityEncryption.Base.Entities;
using EntityEncryption.Base.IVGenerators;

namespace EntityEncryption.NHibernate.Listeners
{
    public class DefaultEncryptionListener : BaseEncryptionListener
    {
        private readonly string _encryptionKey;
        private readonly IDataEncryptor _dataEncryptor;

        public DefaultEncryptionListener(string encryptionKey, IDataEncryptor dataEncryptor = null)
        {
            _encryptionKey = encryptionKey;
            _dataEncryptor = dataEncryptor ?? new AESDataEncryptor(new AESIVGenerator());
        }

        public override void Decrypt(object entity, object[] state, string[] propertyNames)
        {
            var encryptableEntity = entity as IEncryptableEntity;
            if (encryptableEntity == null)
                return;

            _dataEncryptor.DecryptProperties(encryptableEntity, _encryptionKey);

            CopyEntityToState(entity, state, propertyNames);
        }

        public override void Encrypt(object entity, object[] state, string[] propertyNames)
        {
            var encryptableEntity = entity as IEncryptableEntity;
            if (encryptableEntity == null)
                return;
            
            _dataEncryptor.EncryptProperties(encryptableEntity, _encryptionKey);

            CopyEntityToState(entity, state, propertyNames);
        }

        private static void CopyEntityToState(object entity, object[] state, string[] propertyNames)
        {
            for (var i = 0; i < state.Length; i++)
            {
                state[i] = entity.GetType().GetProperty(propertyNames[i]).GetValue(entity, null);
            }
        }
    }
}
