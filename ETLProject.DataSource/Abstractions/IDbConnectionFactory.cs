using ETLProject.Common.Database;
using System.Data;

namespace ETLProject.DataSource.Abstractions
{
    internal interface IDbConnectionFactory
    {
        DataSourceType DataSourceType { get; }
        IDbConnection GetConnection(DatabaseConnectionParameters databaseConnection);
    }
}
