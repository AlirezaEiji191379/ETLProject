using ETLProject.Common.Database;
using System.Data;

namespace ETLProject.DataSource.Query.Abstractions
{
    internal interface IDbConnectionProvider
    {
        IDbConnection GetConnection(DatabaseConnectionParameters databaseConnection);
    }
}
