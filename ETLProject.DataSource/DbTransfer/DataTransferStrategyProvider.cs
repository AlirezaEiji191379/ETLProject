using ETLProject.Common.Database;
using ETLProject.DataSource.Abstractions;
using ETLProject.DataSource.DbTransfer.Configs;
using ETLProject.Infrastructure.Entities;

namespace ETLProject.DataSource.DbTransfer;

internal class DataTransferStrategyProvider : IDataTransferStrategyProvider
{
    private readonly IEnumerable<IDataTransferStrategy> _dataTransferStrategies;

    public DataTransferStrategyProvider(IEnumerable<IDataTransferStrategy> dataTransferStrategies)
    {
        _dataTransferStrategies = dataTransferStrategies;
    }

    public IDataTransferStrategy GetDataTransferStrategy(DataTransferParameter dataTransferParameter)
    {
        var dataTransferType = GetDataTransferType(dataTransferParameter.SourceTable.DatabaseConnection,
            dataTransferParameter.DestinationTable.DatabaseConnection);
        return _dataTransferStrategies
            .First(x => x.DataTransferType == dataTransferType &&
                        x.DataTransferAction == dataTransferParameter.DataTransferAction);
    }


    private DataTransferType GetDataTransferType(DatabaseConnectionParameters source,
        DatabaseConnectionParameters destination)
    {
        var areConnectionsEqual = source.DataSourceType == destination.DataSourceType &&
                                  source.Host == destination.Host &&
                                  source.Port == destination.Port &&
                                  source.Username == destination.Username &&
                                  source.Password == destination.Password;
        return areConnectionsEqual ? DataTransferType.AmongOneConnection : DataTransferType.BetweenTwoConnections;
    }
}