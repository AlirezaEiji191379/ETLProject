using System.Data;
using ETLProject.Common.Table;
using ETLProject.Contract.DbConnectionContracts;
using ETLProject.DataSource.Abstractions;
using ETLProject.DataSource.Common.Exceptions;
using ETLProject.DataSource.DbTransfer.Configs;

namespace ETLProject.DataSource.DbTransfer.Strategies;

internal class BetweenTwoServerInsert : IDataTransferStrategy
{
    private readonly IDataBulkCopyProvider _dataBulkCopyProvider;
    private readonly IDataBaseBulkReader _dataBulkReader;
    private readonly IDbConnectionProvider _connectionProvider;
    private readonly IDbConnectionMetaDataBusinessProvider _connectionMetaDataBusinessProvider;

    public BetweenTwoServerInsert(IDataBulkCopyProvider dataBulkCopyProvider, IDataBaseBulkReader dataBulkReader,
        IDbConnectionProvider connectionProvider,
        IDbConnectionMetaDataBusinessProvider connectionMetaDataBusinessProvider)
    {
        _dataBulkCopyProvider = dataBulkCopyProvider;
        _dataBulkReader = dataBulkReader;
        _connectionProvider = connectionProvider;
        _connectionMetaDataBusinessProvider = connectionMetaDataBusinessProvider;
    }

    public DataTransferType DataTransferType => DataTransferType.BetweenTwoConnections;
    public DataTransferAction DataTransferAction => DataTransferAction.Insert;

    public async Task TransferData(DataTransferParameter dataTransferParameter)
    {
        var destinationDbType = dataTransferParameter.DestinationTable.DataSourceType;
        var destinationDataInserter = _dataBulkCopyProvider.GetBulkInserter(destinationDbType);
        PrepareConnection(dataTransferParameter);
        if (dataTransferParameter.DestinationTable.Columns == null)
        {
            await PrepareDestinationTableColumns(dataTransferParameter.DestinationTable);
        }
        await foreach (var dt in _dataBulkReader.ReadDataInBulk(dataTransferParameter.SourceTable,
                           dataTransferParameter.BulkConfiguration))
        {
            await destinationDataInserter.InsertBulk(dt, dataTransferParameter.DestinationTable);
        }
    }

    private async Task PrepareDestinationTableColumns(ETLTable destinationTable)
    {
        var tableColumns = await _connectionMetaDataBusinessProvider
            .GetMetaDataBusiness(destinationTable.DataSourceType)
            .GetTableColumns(new ConnectionDto()
            {
                DataSourceType = destinationTable.DataSourceType,
                Host = destinationTable.DatabaseConnection.Host,
                Port = destinationTable.DatabaseConnection.Port,
                Username = destinationTable.DatabaseConnection.Username,
                Password = destinationTable.DatabaseConnection.Password,
                DatabaseName = destinationTable.DatabaseConnection.DatabaseName
            },destinationTable.TableName);
        
        if (tableColumns.Count == 0)
        {
            throw new TableNotFoundException(destinationTable.TableName);
        }
        destinationTable.Columns = tableColumns.Select(x => new ETLColumn()
        {
            Name = x.ColumnName
        }).ToList();
    }

    private void PrepareConnection(DataTransferParameter dataTransferParameter)
    {
        var etlTable = dataTransferParameter.DestinationTable;
        if (etlTable.DbConnection == null)
        {
            etlTable.DbConnection = _connectionProvider.GetConnection(etlTable.DatabaseConnection);
        }

        if (etlTable.DbConnection.State != ConnectionState.Open)
            etlTable.DbConnection.Open();
    }
}