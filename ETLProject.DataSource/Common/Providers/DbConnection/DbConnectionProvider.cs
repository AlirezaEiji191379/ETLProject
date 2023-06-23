using ETLProject.Common.Database;
using ETLProject.DataSource.Abstractions;
using ETLProject.DataSource.Common.Assembly;
using System.Data;

namespace ETLProject.DataSource.Common.Providers.DbConnection
{
    internal class DbConnectionProvider : IDbConnectionProvider
    {
        private Dictionary<DataSourceType, IDbConnectionFactory> _connectionStringCreatorsByDatabaseType;
        public DbConnectionProvider()
        {
            InitDictionary();
        }

        private void InitDictionary()
        {
            var connectionStringFactories = typeof(IAssemblyMarker)
                                            .Assembly
                                            .DefinedTypes
                                            .Where(type => !type.IsAbstract && !type.IsInterface && !type.IsAssignableTo(typeof(IDbConnectionFactory)))
                                            .Select(Activator.CreateInstance)
                                            .Cast<IDbConnectionFactory>();
            _connectionStringCreatorsByDatabaseType = new Dictionary<DataSourceType, IDbConnectionFactory>();
            foreach (var factory in connectionStringFactories)
            {
                _connectionStringCreatorsByDatabaseType[factory.DataSourceType] = factory;
            }
        }

        public IDbConnection GetConnection(DatabaseConnectionParameters databaseConnection)
        {
            return _connectionStringCreatorsByDatabaseType[databaseConnection.DataSourceType].GetConnection(databaseConnection);
        }
    }
}
