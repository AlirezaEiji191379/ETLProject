using ETLProject.Common.Database;
using ETLProject.DataSource.Abstractions;
using ETLProject.Infrastructure.Entities;

namespace ETLProject.DataSource.DbTransfer;

internal class DataTransferDecisionMaker : IDataTransferDecisionMaker
{
    public DataTransferDecision MakeDecision(EtlConnection etlConnection, DatabaseConnectionParameters databaseConnectionParameters)
    {
        if (AreConnectionsEqual(etlConnection, databaseConnectionParameters))
            return DataTransferDecision.AmongOneConnection;
        return DataTransferDecision.BetweenTwoConnections;
    }

    private static bool AreConnectionsEqual(EtlConnection etlConnection, DatabaseConnectionParameters databaseConnectionParameters)
    {
        return etlConnection.Host == databaseConnectionParameters.Host && etlConnection.Port == databaseConnectionParameters.Port &&
               etlConnection.Username == databaseConnectionParameters.Username && etlConnection.Password == databaseConnectionParameters.Password &&
               etlConnection.DataSourceType == etlConnection.DataSourceType;
    }
}