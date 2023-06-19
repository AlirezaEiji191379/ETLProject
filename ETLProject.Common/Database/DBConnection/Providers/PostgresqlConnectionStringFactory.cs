using ETLProject.Common.Abstractions;

namespace ETLProject.Common.Database.DBConnection.Providers
{
    internal class PostgresqlConnectionStringFactory : IConnectionStringFactory
    {
        public DataSourceType DataSourceType { get; } = DataSourceType.Postgresql;
        public string GetConnectionString(DatabaseConnection databaseConnection)
        {
            return $"User ID={databaseConnection.Username};Password={databaseConnection.Password};Host={databaseConnection.Host};Port={databaseConnection.Port};Database={databaseConnection.DatabaseName};";
        }
    }
}
