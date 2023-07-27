using ETLProject.Common.Database;
using ETLProject.Infrastructure.Entities;

namespace ETLProject.DataSource.Abstractions;

internal interface IDatabaseConnectionParameterAdapter
{
    DatabaseConnectionParameters CreateDatabaseConnectionParameters(EtlConnection etlConnection);
}