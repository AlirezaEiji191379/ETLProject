using ETLProject.Common.Abstractions;
using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.Contract.DbWriter;
using ETLProject.DataSource.Abstractions;
using ETLProject.DataSource.Common.Exceptions;
using ETLProject.DataSource.DbTransfer;
using ETLProject.DataSource.QueryBusiness.DbAddBusiness.Abstractions;
using ETLProject.Infrastructure.Entities;
using ETLProject.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ETLProject.DataSource.QueryBusiness.DbAddBusiness;

internal class DbAddBusiness : IDbAddBusiness
{
    private readonly IDataTransfer _dataTransfer;
    private readonly IDataRepository<EtlConnection> _connectionRepository;
    private readonly IDataTransferDecisionMaker _dataTransferDecisionMaker;
    private readonly IRandomStringGenerator _stringGenerator;

    public DbAddBusiness(IDataTransfer dataTransfer,
        IDataRepository<EtlConnection> connectionRepository,
        IDataTransferDecisionMaker dataTransferDecisionMaker,
        IRandomStringGenerator stringGenerator)
    {
        _dataTransfer = dataTransfer;
        _connectionRepository = connectionRepository;
        _dataTransferDecisionMaker = dataTransferDecisionMaker;
        _stringGenerator = stringGenerator;
    }

    public async Task WriteToTable(ETLTable inputTable, DbWriterParameter dbWriterParameter)
    {
        if (dbWriterParameter.TableType == TableType.Permanent && string.IsNullOrEmpty(dbWriterParameter.NewTableName))
        {
            throw new ArgumentNullException(nameof(dbWriterParameter.NewTableName));
        }

        var destinationConnection = await _connectionRepository
            .FindByCondition(x => x.Id == dbWriterParameter.DestinationConnectionId)
            .FirstOrDefaultAsync();

        if (destinationConnection == null)
        {
            throw new ConnectionNotFoundException(dbWriterParameter.DestinationConnectionId.ToString());
        }

        var decision = _dataTransferDecisionMaker.MakeDecision(destinationConnection, inputTable.DatabaseConnection);
        if (decision == DataTransferDecision.BetweenTwoConnections)
        {
            var destinationEtlTable = new ETLTable()
            {
                DataSourceType = destinationConnection.DataSourceType,
                TableType = dbWriterParameter.TableType,
                TableName = SetTableName(dbWriterParameter, destinationConnection),
                DatabaseConnection = new DatabaseConnectionParameters()
                {
                    DataSourceType = destinationConnection.DataSourceType,
                    DatabaseName = destinationConnection.DatabaseName,
                    Password = destinationConnection.Password,
                    Username = destinationConnection.Username,
                    Host = destinationConnection.Host,
                    Port = destinationConnection.Port
                }
            };
            await _dataTransfer.TransferDataBetweenTwoDifferentConnections(inputTable, destinationEtlTable, dbWriterParameter.BulkConfiguration);
        }
        else
        {
            var tableName = SetTableName(dbWriterParameter, destinationConnection);
            await _dataTransfer.TransferDataInSingleConnection(inputTable, tableName, dbWriterParameter.TableType);
        }
    }

    private string SetTableName(DbWriterParameter dbWriterParameter, EtlConnection destinationConnection)
    {
        return dbWriterParameter.TableType == TableType.Temp ? GenerateTempTableName(destinationConnection.DataSourceType) : dbWriterParameter.NewTableName;
    }

    private string GenerateTempTableName(DataSourceType dataSourceType)
    {
        var tableName = "ETL_" + _stringGenerator.GenerateRandomString();
        if (dataSourceType == DataSourceType.SQLServer)
            tableName = "#" + tableName;
        return tableName;
    }
}