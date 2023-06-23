using ETLProject.Common.Database;
using System.Data;

namespace ETLProject.DataSource.Query.Abstractions
{
    public interface IDbConnectionProvider
    {
        IDbConnection GetConnection(DatabaseConnectionParameters databaseConnection);
    }
}
