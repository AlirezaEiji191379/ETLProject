using ETLProject.DataSource.Abstractions;
using ETLProject.DataSource.DbTransfer.Configs;

namespace ETLProject.DataSource.DbTransfer.Strategies;

public class BetweenTwoServerInsert : IDataTransferStrategy
{
    private readonly IDataBulkCopyProvider _dataBulkCopyProvider;
    private readonly IDataBaseBulkReader _dataBulkReader;

    public BetweenTwoServerInsert(IDataBulkCopyProvider dataBulkCopyProvider, IDataBaseBulkReader dataBulkReader)
    {
        _dataBulkCopyProvider = dataBulkCopyProvider;
        _dataBulkReader = dataBulkReader;
    }

    public DataTransferType DataTransferType => DataTransferType.BetweenTwoConnections;
    public DataTransferAction DataTransferAction => DataTransferAction.Insert;

    public async Task TransferData(DataTransferParameter dataTransferParameter)
    {
        var destinationDbType = dataTransferParameter.DestinationTable.DataSourceType;
        var destinationDataInserter = _dataBulkCopyProvider.GetBulkInserter(destinationDbType);
        await foreach (var dt in _dataBulkReader.ReadDataInBulk(dataTransferParameter.SourceTable, dataTransferParameter.BulkConfiguration))
        {
            await destinationDataInserter.InsertBulk(dt, dataTransferParameter.DestinationTable);
        }
    }
}