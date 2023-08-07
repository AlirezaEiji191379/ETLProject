using ETLProject.Common.Abstractions;
using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.Contract.DbWriter;
using ETLProject.Contract.DbWriter.Enums;
using ETLProject.DataSource.Abstractions;
using ETLProject.DataSource.Common.Exceptions;
using ETLProject.DataSource.DbTransfer;
using ETLProject.DataSource.DbTransfer.Configs;
using ETLProject.DataSource.QueryBusiness.DbAddBusiness.Abstractions;
using ETLProject.Infrastructure.Entities;
using ETLProject.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ETLProject.DataSource.QueryBusiness.DbAddBusiness;

internal class DbAddBusiness : IDbAddBusiness
{
    private readonly IDataRepository<EtlConnection> _connectionRepository;
    private readonly IDataTransferStrategyProvider _dataTransferStrategyProvider;
    private readonly IRandomStringGenerator _stringGenerator;
    private readonly IDatabaseConnectionParameterAdapter _databaseConnectionParameterAdapter;

    public DbAddBusiness(IDataRepository<EtlConnection> connectionRepository,
        IDataTransferStrategyProvider dataTransferStrategyProvider,
        IRandomStringGenerator stringGenerator,
        IDatabaseConnectionParameterAdapter databaseConnectionParameterAdapter)
    {
        _connectionRepository = connectionRepository;
        _dataTransferStrategyProvider = dataTransferStrategyProvider;
        _stringGenerator = stringGenerator;
        _databaseConnectionParameterAdapter = databaseConnectionParameterAdapter;
    }

    public async Task WriteToTable(ETLTable inputTable, DbWriterParameter dbWriterParameter)
    {
        var dataTransferParameter = new DataTransferParameter();
        dataTransferParameter.DataTransferAction = dbWriterParameter.DbTransferAction == DbTransferAction.Insert
            ? DataTransferAction.Insert
            : DataTransferAction.CreateInsert;
        dataTransferParameter.BulkConfiguration = dbWriterParameter.BulkConfiguration;
        
        DatabaseConnectionParameters destinationConnection;
        if (!dbWriterParameter.UseInputConnection)
        {
            try
            {
                var etlConnection = await _connectionRepository
                    .FindByCondition(x => x.Id == dbWriterParameter.DestinationConnectionId)
                    .FirstOrDefaultAsync();
                destinationConnection = _databaseConnectionParameterAdapter.CreateDatabaseConnectionParameters(etlConnection);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        else
        {
            destinationConnection = inputTable.DatabaseConnection;
        }

        if (destinationConnection == null)
        {
            throw new ConnectionNotFoundException();
        }

        dataTransferParameter.DestinationTable = new ETLTable()
        {
            TableName = dbWriterParameter.DestinationTableName,
            DatabaseConnection = destinationConnection,
            TableType = TableType.Permanent,
            DataSourceType = destinationConnection.DataSourceType,
            TableSchema = dbWriterParameter.DestinationTableSchema,
        };
        dataTransferParameter.SourceTable = inputTable;
        var dataTransferStrategy = _dataTransferStrategyProvider.GetDataTransferStrategy(dataTransferParameter);
        await dataTransferStrategy.TransferData(dataTransferParameter);
    }


    private string GenerateTempTableName(DataSourceType dataSourceType)
    {
        var tableName = "ETL_" + _stringGenerator.GenerateRandomString();
        if (dataSourceType == DataSourceType.SQLServer)
            tableName = "#" + tableName;
        return tableName;
    }
}