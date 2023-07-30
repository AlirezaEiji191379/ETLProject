using System.Text;
using ETLProject.Common.Abstractions;
using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.Contract.DbWriter;
using ETLProject.Contract.Join;
using ETLProject.DataSource.Abstractions;
using ETLProject.DataSource.DbTransfer.Configs;
using ETLProject.DataSource.QueryBusiness.JoinBusiness.Abstractions;

namespace ETLProject.DataSource.QueryBusiness.JoinBusiness;

internal class JoinQueryBusiness : IJoinQueryBusiness
{
    private readonly IDataTransferStrategyProvider _dataTransferStrategyProvider;
    private readonly IRandomStringGenerator _randomStringGenerator;
    private readonly IJoinValidator _validator;
    private readonly IJoinQueryMaker _joinQueryMaker;

    public JoinQueryBusiness(IDataTransferStrategyProvider dataTransferStrategyProvider,
        IRandomStringGenerator randomStringGenerator,
        IJoinValidator validator,
        IJoinQueryMaker joinQueryMaker)
    {
        _dataTransferStrategyProvider = dataTransferStrategyProvider;
        _randomStringGenerator = randomStringGenerator;
        _validator = validator;
        _joinQueryMaker = joinQueryMaker;
    }

    public async Task<ETLTable> JoinTables(ETLTable leftTable, ETLTable rightTable, JoinParameter joinParameter)
    {
        _validator.ValidateJoinParameter(leftTable, rightTable, joinParameter);
        var tableToTransfer = joinParameter.UseLeftTableConnection ? rightTable : leftTable;
        var otherTable = joinParameter.UseLeftTableConnection ? leftTable : rightTable;
        var transferredTable =
            await TransferTable(tableToTransfer, otherTable, joinParameter.BulkConfiguration);

        var resultTable = PrepareJoinResultTable(transferredTable, otherTable, joinParameter);
        return resultTable;
    }
    private ETLTable PrepareJoinResultTable(ETLTable transferredTable, ETLTable otherTable, JoinParameter joinParameter)
    {
        var isLeftTableConnection = joinParameter.UseLeftTableConnection;
        var leftTable = isLeftTableConnection ? otherTable : transferredTable;
        var rightTable = isLeftTableConnection ? transferredTable : otherTable;
        var resultTable = new ETLTable();
        PrepareResultTableColumns(leftTable, rightTable, joinParameter, resultTable);
        resultTable.TableSchema = otherTable.TableSchema;
        resultTable.TableName = _randomStringGenerator.GenerateRandomString(10);
        resultTable.AliasName = _randomStringGenerator.GenerateRandomString(8);
        resultTable.TableType = TableType.Temp;
        resultTable.DatabaseConnection = otherTable.DatabaseConnection;
        resultTable.DbConnection = otherTable.DbConnection;
        resultTable.DataSourceType = otherTable.DataSourceType;
        _joinQueryMaker.AddJoinQueryToResultTable(leftTable, rightTable, resultTable, joinParameter);
        return resultTable;
    }
    private static void PrepareResultTableColumns(ETLTable leftTable, ETLTable rightTable,
        JoinParameter joinParameter, ETLTable resultTable)
    {
        resultTable.Columns = new List<ETLColumn>();
        foreach (var leftTableSelectedColumn in joinParameter.LeftTableSelectedColumns)
        {
            var clonedColumn = leftTable.Columns.First(x => x.Name == leftTableSelectedColumn.ColumnName).Clone();
            clonedColumn.Name = leftTableSelectedColumn.OutputTitle;
            resultTable.Columns.Add(clonedColumn);
        }
        foreach (var rightTableSelectedColumn in joinParameter.RigthTableSelectedColumns)
        {
            var clonedColumn = rightTable.Columns.First(x => x.Name == rightTableSelectedColumn.ColumnName).Clone();
            clonedColumn.Name = rightTableSelectedColumn.OutputTitle;
            resultTable.Columns.Add(clonedColumn);
        }
    }
    private async Task<ETLTable> TransferTable(ETLTable tableToTransfer, ETLTable otherTable,
        BulkConfiguration bulkConfiguration)
    {
        if (_dataTransferStrategyProvider.GetDataTransferType(tableToTransfer.DatabaseConnection,
                otherTable.DatabaseConnection) == DataTransferType.AmongOneConnection)
        {
            return tableToTransfer;
        }
        var transferredResultTable = new ETLTable();
        PrepareTransferredTable(tableToTransfer, otherTable, transferredResultTable);

        var dataTransferParameter = new DataTransferParameter()
        {
            BulkConfiguration = bulkConfiguration ?? new BulkConfiguration() { BatchSize = 100 },
            DataTransferAction = DataTransferAction.CreateInsert,
            SourceTable = tableToTransfer,
            DestinationTable = transferredResultTable
        };
        var dataTransferStrategy = _dataTransferStrategyProvider.GetDataTransferStrategy(dataTransferParameter);
        await dataTransferStrategy.TransferData(dataTransferParameter);
        return transferredResultTable;
    }
    private void PrepareTransferredTable(ETLTable tableToTransfer, ETLTable otherTable, ETLTable resultTable)
    {
        resultTable.TableName = GenerateRandomTempTableName(otherTable);
        resultTable.TableSchema = otherTable.TableSchema;
        resultTable.AliasName = _randomStringGenerator.GenerateRandomString(8);
        resultTable.DbConnection = otherTable.DbConnection;
        resultTable.DatabaseConnection = otherTable.DatabaseConnection;
        resultTable.Columns = tableToTransfer.Columns.Select(x => x.Clone()).ToList();
        resultTable.DataSourceType = otherTable.DataSourceType;
        resultTable.TableType = TableType.Temp;
    }
    private string GenerateRandomTempTableName(ETLTable otherTable)
    {
        var randomName = _randomStringGenerator.GenerateRandomString(10);
        var tableName = new StringBuilder(otherTable.DataSourceType == DataSourceType.SQLServer ? "#" : string.Empty)
            .Append(randomName).ToString();
        return tableName;
    }
}