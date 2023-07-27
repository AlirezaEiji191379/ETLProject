using ETLProject.Common.Database;
using ETLProject.DataSource.Abstractions;
using ETLProject.Infrastructure.Entities;

namespace ETLProject.DataSource.Common.Adapters;

internal class DatabaseConnectionParameterAdapter : IDatabaseConnectionParameterAdapter
{
    public DatabaseConnectionParameters CreateDatabaseConnectionParameters(EtlConnection etlConnection)
    {
        return new DatabaseConnectionParameters()
        {
            Id = etlConnection.Id,
            Host = etlConnection.Host,
            Port = etlConnection.Port,
            DataSourceType = etlConnection.DataSourceType,
            Password = etlConnection.Password,
            Username = etlConnection.Username,
            DatabaseName = etlConnection.DatabaseName
        };
    }
}