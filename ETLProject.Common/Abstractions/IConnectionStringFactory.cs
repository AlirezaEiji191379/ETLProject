using ETLProject.Common.Database;
using ETLProject.Common.Database.DBConnection;

namespace ETLProject.Common.Abstractions
{
    internal interface IConnectionStringFactory
    {
        DataSourceType DataSourceType { get; }
        string GetConnectionString(DatabaseConnectionParameters databaseConnection);
    }
}
