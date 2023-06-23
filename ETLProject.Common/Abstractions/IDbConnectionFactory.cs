using ETLProject.Common.Database;
using ETLProject.Common.Database.DBConnection;
using System.Data;

namespace ETLProject.Common.Abstractions
{
    internal interface IDbConnectionFactory
    {
        DataSourceType DataSourceType { get; }
        IDbConnection GetConnection(DatabaseConnectionParameters databaseConnection);
    }
}
