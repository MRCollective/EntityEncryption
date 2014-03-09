namespace EntityEncryption.NHibernate.Listeners
{
    public interface IEncryptionListener
    {
        void Decrypt(object entity, object[] state, string[] propertyNames);
        void Encrypt(object entity, object[] state, string[] propertyNames);
    }
}