using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace SStudio.BudgetManager.Web.API.Data
{
    public interface ISessionProvider
    {
        ISessionFactory GetSession<T>();
    }
    
    public class SessionProvider : ISessionProvider
    {
        private readonly string host = "localhost";
        private readonly int port = 5432;
        private readonly string databaseName = "Budget";
        private readonly string login = "postgres";
        private readonly string password = "sstudio_dba1";
        
        public ISessionFactory GetSession<T>()
        {
            return GetDatabase()
                    .Mappings(m => m.FluentMappings.Add<T>())
                    .ExposeConfiguration(TreatConfiguration)
                    .BuildSessionFactory();
        }

        public ISessionFactory GetSession(Type type)
        {
            return GetDatabase()
                    .Mappings(m => m.FluentMappings.Add(type))
                    .ExposeConfiguration(TreatConfiguration)
                    .BuildSessionFactory();
        }

        public ISessionFactory GetSession(Type[] types)
        {
            var database = GetDatabase();

            foreach (var type in types)
            {
                database.Mappings(m => m.FluentMappings.Add(type));
            }

            return database.ExposeConfiguration(TreatConfiguration).BuildSessionFactory();
        }

        private FluentConfiguration GetDatabase()
        {
            return Fluently
                        .Configure()
                        .Database(
                            PostgreSQLConfiguration.Standard
                            .ConnectionString(c =>
                                c.Host(host)
                                .Port(port)
                                .Database(databaseName)
                                .Username(login)
                                .Password(password)
                            )
                        );
        }

        private void TreatConfiguration(Configuration configuration)
        {
            // dump sql file for debug
            Action<string> updateExport = x =>
            {
                using (var file = new FileStream(@"update.sql", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                using (var sw = new StreamWriter(file))
                {
                    sw.Write(x);
                    sw.Close();
                }
            };
            var update = new SchemaUpdate(configuration);
            update.Execute(updateExport, true);
        }

    }
}
