using EntityEncryption.NHibernate.Listeners;
using EntityEncryption.NHibernate.Registration;
using NHibernate.Cfg;
using Xunit;

namespace EntityEncryption.NHibernate.Tests.Registration
{
    public class ConfigurationExtensionsTests
    {
        [Fact]
        public void Register_default_listener()
        {
            var configuration = new Configuration();
            var listener = new DefaultEncryptionListener(null);
            
            configuration.RegisterEncryption(listener);

            Assert.Single(configuration.EventListeners.PreUpdateEventListeners, listener);
            Assert.Single(configuration.EventListeners.PostUpdateEventListeners, listener);
            Assert.Single(configuration.EventListeners.PreInsertEventListeners, listener);
            Assert.Single(configuration.EventListeners.PostInsertEventListeners, listener);
            Assert.Single(configuration.EventListeners.PreLoadEventListeners, listener);
        }
    }
}