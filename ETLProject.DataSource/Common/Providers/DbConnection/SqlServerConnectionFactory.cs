using ETLProject.Common.Database;
using ETLProject.DataSource.Abstractions;
using System.Data;
using System.Data.SqlClient;

namespace ETLProject.DataSource.Common.Providers.DbConnection
{
    internal class SqlServerConnectionFactory : IDbConnectionFactory
    {
        public DataSourceType DataSourceType { get; } = DataSourceType.SQLServer;

        public IDbConnection GetConnection(DatabaseConnectionParameters databaseConnection)
        {
            var connString = databaseConnection.DatabaseName != null
                ? $"Server={databaseConnection.Host};Database={databaseConnection.DatabaseName};User Id ={databaseConnection.Username};Password={databaseConnection.Password}"
                : $"Server={databaseConnection.Host};User Id ={databaseConnection.Username};Password={databaseConnection.Password}";
            return new SqlConnection(connString);
        }
    }
}