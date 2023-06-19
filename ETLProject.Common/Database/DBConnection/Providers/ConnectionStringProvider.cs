using ETLProject.Common.Abstractions;

namespace ETLProject.Common.Database.DBConnection.Providers
{
    internal class ConnectionStringProvider : IConnectionStringProvider
    {
        private readonly Dictionary<DataSourceType, IConnectionStringFactory> _connectionStringCreatorsByDatabaseType;

        internal ConnectionStringProvider(IEnumerable<IConnectionStringFactory> connectionStringFactories)
        {
            _connectionStringCreatorsByDatabaseType = new Dictionary<DataSourceType, IConnectionStringFactory>();
            foreach(var factory in connectionStringFactories)
            {
                _connectionStringCreatorsByDatabaseType[factory.DataSourceType] = factory;
            }
        }

        public string GetConnectionString(DatabaseConnection databaseConnection)
        {
            return _connectionStringCreatorsByDatabaseType[databaseConnection.DataSourceType].GetConnectionString(databaseConnection);
        }
    }
}
