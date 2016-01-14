using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

using DataItem = SStudio.BudgetManager.Web.API.Data.Item;
using SStudio.BudgetManager.Web.API.Models;

namespace SStudio.BudgetManager.Web.API.Repository
{
    public interface IItemContext
    {
        IEnumerable<Item> GetAll();
        int Create(Item item);
        bool Delete(int id);
    }

    public class ItemContext : IItemContext
    {
        private readonly string host = "localhost";
        private readonly int port = 5432;
        private readonly string databaseName = "Budget";
        private readonly string login = "postgres";
        private readonly string password = "sstudio_dba1";

        IEnumerable<Item> IItemContext.GetAll()
        {
            var sessionFactory = CreateSessionFactory();

            using (var session = sessionFactory.OpenSession())
            {
                using (session.BeginTransaction())
                {
                    var items = session.CreateCriteria(typeof(DataItem))
                                       .List<DataItem>()
                                       .Select(i => new Item { Id = i.Id, Name = i.Name });
                    return items;
                }
            }
        }

        int IItemContext.Create(Item item)
        {
            var sessionFactory = CreateSessionFactory();
            var dataItem = new DataItem { Name = item.Name };

            using (var session = sessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    
                    session.SaveOrUpdate(dataItem);
                    trans.Commit();
                }
            }

            return dataItem.Id;
        }

        public bool Delete(int id)
        {
            try
            {
                var sessionFactory = CreateSessionFactory();

                using (var session = sessionFactory.OpenSession())
                {
                    using (var trans = session.BeginTransaction())
                    {
                        var item = session.Get(typeof(DataItem), id);
                        session.Delete(item);
                        trans.Commit();
                    }
                }
            }
            catch(Exception e)
            {
                return false;
            }

            return true;
        }

        private ISessionFactory CreateSessionFactory()
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
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Data.ItemMap>())
                    .ExposeConfiguration(TreatConfiguration)
                .BuildSessionFactory();
        }
        private static void TreatConfiguration(Configuration configuration)
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
