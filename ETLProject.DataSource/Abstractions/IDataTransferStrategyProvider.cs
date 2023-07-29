using ETLProject.Common.Database;
using ETLProject.DataSource.DbTransfer.Configs;

namespace ETLProject.DataSource.Abstractions;

internal interface IDataTransferStrategyProvider
{
    IDataTransferStrategy GetDataTransferStrategy(DataTransferParameter dataTransferParameter);
    DataTransferType GetDataTransferType(DatabaseConnectionParameters source, DatabaseConnectionParameters destination);
}