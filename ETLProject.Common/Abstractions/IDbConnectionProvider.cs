using ETLProject.Common.Database.DBConnection;
using System.Data;

namespace ETLProject.Common.Abstractions
{
    public interface IDbConnectionProvider
    {
        IDbConnection GetConnection(DatabaseConnectionParameters databaseConnection);
    }
}
