using ETLProject.DataSource.DbTransfer.Configs;

namespace ETLProject.DataSource.Abstractions;

internal interface IDataTransferStrategyProvider
{
    IDataTransferStrategy GetDataTransferStrategy(DataTransferParameter dataTransferParameter);
}