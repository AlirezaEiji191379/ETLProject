using ETLProject.Common.Abstractions;
using Npgsql;
using System.Data;

namespace ETLProject.Common.Database.DBConnection.Providers
{
    internal class PostgresqlConnectionFactory : IDbConnectionFactory
    {
        public DataSourceType DataSourceType { get; } = DataSourceType.Postgresql;
        public IDbConnection GetConnection(DatabaseConnectionParameters databaseConnection)
        {
            var connString = $"User ID={databaseConnection.Username};Password={databaseConnection.Password};Host={databaseConnection.Host};Port={databaseConnection.Port};Database={databaseConnection.DatabaseName};";
            return new NpgsqlConnection(connString);
        }
    }
}
