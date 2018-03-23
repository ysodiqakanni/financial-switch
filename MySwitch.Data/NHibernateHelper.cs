using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MySwitch.Core.Maps;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.Data
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    InitializeSessionFactory();
                return _sessionFactory;
            }
        }
        private static void InitializeSessionFactory()
        {
            _sessionFactory = Fluently.Configure().Database(MsSqlConfiguration.MsSql2008.ConnectionString(@"Data Source=.; Initial Catalog=SwitchSodiqDb; Integrated Security=true").ShowSql()).Mappings(m => m.FluentMappings.AddFromAssemblyOf<FeeMap>())
            .ExposeConfiguration(cfg => new SchemaUpdate(cfg)
            .Execute(false, true))       //(true, true) => all db data gets deleted evrytime app is run
            .BuildSessionFactory();
        }
        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
