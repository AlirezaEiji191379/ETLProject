using ETLProject.Common.Abstractions;

namespace ETLProject.Common.Database.DBConnection.Providers
{
    internal class SqlServerConnectionStringFactory : IConnectionStringFactory
    {
        public DataSourceType DataSourceType { get; } = DataSourceType.SQLServer;
        public string GetConnectionString(DatabaseConnection databaseConnection)
        {
            return $"Server={databaseConnection.Host};Database={databaseConnection.DatabaseName};User Id ={databaseConnection.Username};Password={databaseConnection.Password}";
        }
    }
}
