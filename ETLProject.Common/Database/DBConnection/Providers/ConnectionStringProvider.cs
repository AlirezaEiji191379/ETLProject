using ETLProject.Common.Abstractions;
using ETLProject.Common.Common.AssemblyMarker;

namespace ETLProject.Common.Database.DBConnection.Providers
{
    internal class ConnectionStringProvider : IConnectionStringProvider
    {
        private Dictionary<DataSourceType, IConnectionStringFactory> _connectionStringCreatorsByDatabaseType;
        public ConnectionStringProvider()
        {
            InitDictionary();
        }

        private void InitDictionary()
        {
            var connectionStringFactories = typeof(IAssemblyMarker)
                                            .Assembly
                                            .DefinedTypes
                                            .Where(type => !type.IsAbstract && !type.IsInterface && !type.IsAssignableTo(typeof(IConnectionStringFactory)))
                                            .Select(Activator.CreateInstance)
                                            .Cast<IConnectionStringFactory>();
            _connectionStringCreatorsByDatabaseType = new Dictionary<DataSourceType, IConnectionStringFactory>();
            foreach (var factory in connectionStringFactories)
            {
                _connectionStringCreatorsByDatabaseType[factory.DataSourceType] = factory;
            }
        }

        public string GetConnectionString(DatabaseConnectionParameters databaseConnection)
        {
            return _connectionStringCreatorsByDatabaseType[databaseConnection.DataSourceType].GetConnectionString(databaseConnection);
        }
    }
}
