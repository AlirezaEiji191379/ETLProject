using ETLProject.Common.Abstractions;
using System.Data;
using System.Data.SqlClient;

namespace ETLProject.Common.Database.DBConnection.Providers
{
    internal class SqlServerConnectionFactory : IDbConnectionFactory
    {
        public DataSourceType DataSourceType { get; } = DataSourceType.SQLServer;
        public IDbConnection GetConnection(DatabaseConnectionParameters databaseConnection)
        {
            var connString = $"Server={databaseConnection.Host};Database={databaseConnection.DatabaseName};User Id ={databaseConnection.Username};Password={databaseConnection.Password}";
            return new SqlConnection(connString);
        }
    }
}
