using ETLProject.Common.Database.DBConnection;

namespace ETLProject.Common.Abstractions
{
    public interface IConnectionStringProvider
    {
        string GetConnectionString(DatabaseConnectionParameters databaseConnection);
    }
}
