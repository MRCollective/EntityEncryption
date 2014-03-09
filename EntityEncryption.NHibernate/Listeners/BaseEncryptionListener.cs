using NHibernate.Event;

namespace EntityEncryption.NHibernate.Listeners
{
    public abstract class BaseEncryptionListener : IEncryptionListener, IPreUpdateEventListener, IPostUpdateEventListener, IPreInsertEventListener, IPostInsertEventListener, IPreLoadEventListener
    {
        public abstract void Decrypt(object entity, object[] state, string[] propertyNames);
        public abstract void Encrypt(object entity, object[] state, string[] propertyNames);

        public bool OnPreUpdate(PreUpdateEvent updateEvent)
        {
            Encrypt(updateEvent.Entity, updateEvent.State, updateEvent.Persister.PropertyNames);
            return false;
        }

        public void OnPostUpdate(PostUpdateEvent @event)
        {
            Decrypt(@event.Entity, @event.State, @event.Persister.PropertyNames);
        }

        public bool OnPreInsert(PreInsertEvent insertEvent)
        {
            Encrypt(insertEvent.Entity, insertEvent.State, insertEvent.Persister.PropertyNames);
            return false;
        }

        public void OnPostInsert(PostInsertEvent @event)
        {
            Decrypt(@event.Entity, @event.State, @event.Persister.PropertyNames);
        }

        public void OnPreLoad(PreLoadEvent @event)
        {
            Decrypt(@event.Entity, @event.State, @event.Persister.PropertyNames);
        } 
    }
}