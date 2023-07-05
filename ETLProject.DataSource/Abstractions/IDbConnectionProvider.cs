using ETLProject.Common.Database;
using System.Data;

namespace ETLProject.DataSource.Abstractions
{
    internal interface IDbConnectionProvider
    {
        IDbConnection GetConnection(DatabaseConnectionParameters databaseConnection);
    }
}
