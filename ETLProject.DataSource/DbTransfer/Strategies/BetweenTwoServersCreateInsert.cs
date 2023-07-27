using ETLProject.Common.Table;
using ETLProject.Contract.DbWriter;
using ETLProject.DataSource.Abstractions;
using ETLProject.DataSource.DbTransfer.Configs;

namespace ETLProject.DataSource.DbTransfer.Strategies;

public class BetweenTwoServersCreateInsert : IDataTransferStrategy
{
    private readonly IDbTableFactory _dbTableCreator;
    private readonly IDataBulkCopyProvider _dataBulkCopyProvider;
    private readonly IDataBaseBulkReader _dataBulkReader;
    

    public BetweenTwoServersCreateInsert(IDbTableFactory dbTableCreator, IDataBulkCopyProvider dataBulkCopyProvider, IDataBaseBulkReader dataBulkReader)
    {
        _dbTableCreator = dbTableCreator;
        _dataBulkCopyProvider = dataBulkCopyProvider;
        _dataBulkReader = dataBulkReader;
    }

    public DataTransferType DataTransferType => DataTransferType.BetweenTwoConnections;
    public DataTransferAction DataTransferAction => DataTransferAction.CreateInsert;

    public async Task TransferData(DataTransferParameter dataTransferParameter)
    {
        dataTransferParameter.DestinationTable.Columns = dataTransferParameter.SourceTable.CloneEtlColumns();
        var dataInserter = _dataBulkCopyProvider.GetBulkInserter(dataTransferParameter.DestinationTable.DataSourceType);
        await _dbTableCreator.CreateTable(dataTransferParameter.DestinationTable);
        await foreach (var dt in _dataBulkReader.ReadDataInBulk(dataTransferParameter.SourceTable, dataTransferParameter.BulkConfiguration))
        {
            await dataInserter.InsertBulk(dt, dataTransferParameter.DestinationTable);
        }
    }
}