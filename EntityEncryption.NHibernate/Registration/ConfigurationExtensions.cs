using EntityEncryption.NHibernate.Listeners;
using NHibernate.Cfg;
using NHibernate.Event;

namespace EntityEncryption.NHibernate.Registration
{
    public static class ConfigurationExtensions
    {
        public static void RegisterEncryption(this Configuration config, IEncryptionListener listener = null)
        {
            config.AppendListeners(ListenerType.PreUpdate, new[] { (IPreUpdateEventListener)listener });
            config.AppendListeners(ListenerType.PostUpdate, new[] { (IPostUpdateEventListener)listener });
            config.AppendListeners(ListenerType.PreInsert, new[] { (IPreInsertEventListener)listener });
            config.AppendListeners(ListenerType.PostInsert, new[] { (IPostInsertEventListener)listener });
            config.AppendListeners(ListenerType.PreLoad, new[] { (IPreLoadEventListener)listener });
        } 
    }
}