using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.DataSource.Abstractions;
using ETLProject.DataSource.DbTransfer.Configs;

namespace ETLProject.DataSource.DbTransfer.Strategies;

public class AmongOneServerCreateInsert : IDataTransferStrategy
{
    private readonly IDbTableFactory _dbTableCreator;

    public AmongOneServerCreateInsert(IDbTableFactory dbTableCreator)
    {
        _dbTableCreator = dbTableCreator;
    }

    public DataTransferType DataTransferType => DataTransferType.AmongOneConnection;
    public DataTransferAction DataTransferAction => DataTransferAction.CreateInsert;

    public async Task TransferData(DataTransferParameter dataTransferParameter)
    {
        if (dataTransferParameter.SourceTable.DataSourceType != DataSourceType.SQLServer)
        {
            await _dbTableCreator.CreateTableAs(dataTransferParameter.SourceTable,
                dataTransferParameter.DestinationTable.TableName,
                dataTransferParameter.DestinationTable.TableType);
            return;
        }

        await _dbTableCreator.SelectInto(dataTransferParameter.SourceTable,
            dataTransferParameter.DestinationTable.TableName);
    }
}