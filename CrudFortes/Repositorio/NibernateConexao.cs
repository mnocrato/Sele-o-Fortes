using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.Reflection;
using NHibernate.Tool.hbm2ddl;
using CrudFortes.Entidades;

namespace CrudFortes.Repositorio
{
    public class NibernateConexao
    {
        private static ISessionFactory session;

        private static ISessionFactory CriarSessao()
        {
            if (session != null) return session;

            session = Fluently.Configure()
             .Database(
              MsSqlConfiguration.MsSql2012.ConnectionString(
              c => c.FromConnectionStringWithKey("MyConnectionString")))
              .Mappings(x => x.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
              .BuildSessionFactory();
            return session;
        }

        public static ISession AbrirSessao()
        {
            return CriarSessao().OpenSession();
        }
    }
}
