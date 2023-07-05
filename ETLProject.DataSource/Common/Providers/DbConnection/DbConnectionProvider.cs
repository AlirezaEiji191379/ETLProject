using ETLProject.Common.Database;
using ETLProject.DataSource.Abstractions;
using ETLProject.DataSource.Common.Assembly;
using System.Data;

namespace ETLProject.DataSource.Common.Providers.DbConnection
{
    internal class DbConnectionProvider : IDbConnectionProvider
    {
        private readonly Dictionary<DataSourceType, IDbConnectionFactory> _connectionCreatorsByDatabaseType;
        public DbConnectionProvider()
        {
            _connectionCreatorsByDatabaseType = InitDictionary();
        }

        private Dictionary<DataSourceType, IDbConnectionFactory> InitDictionary()
        {
            var result = new Dictionary<DataSourceType, IDbConnectionFactory>();
            var connectionFactories = typeof(IAssemblyMarker)
                                            .Assembly
                                            .DefinedTypes
                                            .Where(type => !type.IsAbstract && !type.IsInterface && type.IsAssignableTo(typeof(IDbConnectionFactory)))
                                            .Select(Activator.CreateInstance)
                                            .Cast<IDbConnectionFactory>();
            
            foreach (var factory in connectionFactories)
            {
                result[factory.DataSourceType] = factory;
            }
            return result;
        }

        public IDbConnection GetConnection(DatabaseConnectionParameters databaseConnection)
        {
            return _connectionCreatorsByDatabaseType[databaseConnection.DataSourceType].GetConnection(databaseConnection);
        }
    }
}
