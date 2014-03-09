using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EntityEncryption.Base.Attributes;
using EntityEncryption.Base.Encryptors;
using EntityEncryption.Base.Entities;
using EntityEncryption.Base.IVGenerators;
using EntityEncryption.NHibernate.Listeners;
using EntityEncryption.NHibernate.Registration;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Mapping;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using NHibernate.Tool.hbm2ddl;
using Xunit;

namespace EntityEncryption.NHibernate.Tests.Listeners
{
    public class DefaultEncryptionListenerTests : IDisposable
    {
        private ISession _session;

        public DefaultEncryptionListenerTests()
        {
            var listener = new DefaultEncryptionListener("Buj7srPbR73qjUTbXimNAg==", new AESDataEncryptor(new TestIVGenerator()));
            ConfigureTestDatabase(listener);
        }

        [Fact]
        public void Encrypt_new_entity()
        {
            _session.Save(new TestEntity{EncryptedString = "Test1"});

            var storedEntity = (ArrayList)_session.CreateSQLQuery("select EncryptedString from TestEntity").List();
            Assert.Equal("SnJgdkGlkPUOyrNCHKoXYQ==", storedEntity[0]);
        }

        [Fact]
        public void Decrypt_stored_entity()
        {
            _session.Save(new TestEntity { Id = 1, EncryptedString = "Test1" });

            var storedEntity = _session.Get<TestEntity>(1);

            Assert.Equal("Test1", storedEntity.EncryptedString);
        }

        public void Dispose()
        {
            _session.Query<TestEntity>().ForEach(e => _session.Delete(e));
        }

        private class TestIVGenerator : IIVGenerator
        {
            public string NewIV()
            {
                return "Buj7srPbR73qjUTbXimNAg==";
            }
        }

        private class TestEntityMap : ClassMap<TestEntity>
        {
            protected TestEntityMap()
            {
                Id(m => m.Id);
                Map(m => m.EncryptedString);
            }
        }

        private class TestEntity : IEncryptableEntity
        {
            public virtual int Id { get; set; }
            public virtual string Iv { get; set; }
            [Encrypt]
            public virtual string EncryptedString { get; set; }
        }

        private void ConfigureTestDatabase(IEncryptionListener listener)
        {
            SchemaExport schemaExport = null;
            var configuration = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.InMemory().ShowSql())
                .Mappings(m => m.FluentMappings.Add<TestEntityMap>())
                .ExposeConfiguration(c =>
                {
                    c.RegisterEncryption(listener);
                    schemaExport = new SchemaExport(c);
                });

            _session = configuration.BuildSessionFactory().OpenSession();
            schemaExport.Execute(true, true, false, _session.Connection, null);
        }
    }
}