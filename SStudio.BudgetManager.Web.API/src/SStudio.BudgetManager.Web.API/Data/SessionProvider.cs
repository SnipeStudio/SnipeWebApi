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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Mapping Type from Data.Mapping</typeparam>
        /// <returns>Returns SessionFactory to start a transaction</returns>
        public ISessionFactory GetSession<T>()
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
                            .Password(password)))
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<T>())
                    .ExposeConfiguration(TreatConfiguration)
                .BuildSessionFactory();
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
