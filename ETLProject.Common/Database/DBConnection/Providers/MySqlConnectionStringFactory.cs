using ETLProject.Common.Abstractions;

namespace ETLProject.Common.Database.DBConnection.Providers
{
    internal class MySqlConnectionStringFactory : IConnectionStringFactory
    {
        public DataSourceType DataSourceType { get; } = DataSourceType.MySql;
        public string GetConnectionString(DatabaseConnection databaseConnection)
        {
            return $"Server={databaseConnection.Host};Database={databaseConnection.DatabaseName};Uid={databaseConnection.Username};Pwd={databaseConnection.Password};";
        }
    }
}
