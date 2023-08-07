using System.Text;
using ETLProject.Common.Abstractions;
using ETLProject.Common.Table;
using ETLProject.Contract.Aggregate;
using ETLProject.Contract.Aggregate.Enums;
using ETLProject.DataSource.Common.Exceptions;
using ETLProject.DataSource.QueryBusiness.AggregateBusiness.Abstractions;
using ETLProject.DataSource.QueryBusiness.AggregateBusiness.Exceptions;
using Microsoft.Extensions.Primitives;
using SqlKata;

namespace ETLProject.DataSource.QueryBusiness.AggregateBusiness;

internal class AggregateQueryBusiness : IAggregateQueryBusiness
{
    private readonly Dictionary<string, string> _columnNameByColumnAliasName;
    private readonly IRandomStringGenerator _randomStringGenerator;

    public AggregateQueryBusiness(IRandomStringGenerator randomStringGenerator)
    {
        _randomStringGenerator = randomStringGenerator;
        _columnNameByColumnAliasName = new Dictionary<string, string>();
    }

    public ETLTable AddAggregation(ETLTable inputTable, AggregationParameter aggregationParameter)
    {
        var resultTable = new ETLTable();
        resultTable.Columns = new List<ETLColumn>();
        CreateGroupByColumns(inputTable, aggregationParameter, resultTable);
        CreateAggregateColumns(inputTable, aggregationParameter, resultTable);
        CreateNewQuery(inputTable, aggregationParameter, resultTable);
        resultTable.DatabaseConnection = inputTable.DatabaseConnection;
        resultTable.TableName = inputTable.TableName;
        resultTable.TableSchema = inputTable.TableSchema;
        resultTable.TableType = inputTable.TableType;
        resultTable.DataSourceType = inputTable.DataSourceType;
        resultTable.DbConnection = inputTable.DbConnection;
        return resultTable;
    }

    private void CreateNewQuery(ETLTable inputTable, AggregationParameter aggregationParameter, ETLTable resultTable)
    {
        resultTable.AliasName = _randomStringGenerator.GenerateRandomString(10);
        resultTable.Query = new Query()
            .From(inputTable.Query, resultTable.AliasName)
            .Select(aggregationParameter.GroupByColumns.Select(x => resultTable.AliasName + "." + x))
            .GroupBy(aggregationParameter.GroupByColumns.Select(x => resultTable.AliasName + "." + x).ToArray());
        foreach (var aggregateSelect in from aggregateColumn in aggregationParameter.AggregateColumns
                 let aggStringType = aggregateColumn.AggregateType.ToString().ToLower()
                 select new StringBuilder(aggStringType)
                     .Append(
                         $"([{resultTable.AliasName}].[{aggregateColumn.ColumnName}]) as [{_columnNameByColumnAliasName[aggregateColumn.ColumnName]}]")
                     .ToString())
        {
            resultTable.Query.SelectRaw(aggregateSelect);
        }
    }

    private void CreateAggregateColumns(ETLTable inputTable, AggregationParameter aggregationParameter,
        ETLTable resultTable)
    {
        foreach (var aggregateColumn in aggregationParameter.AggregateColumns)
        {
            var etlColumn = inputTable.Columns.FirstOrDefault(x => x.Name == aggregateColumn.ColumnName);
            if (etlColumn == null)
            {
                throw new ColumnDoesNotExistException(
                    $"the column with name {aggregateColumn.ColumnName} does not exist in table!");
            }

            var clonedColumn = etlColumn.Clone();

            if (aggregateColumn.AggregateType == AggregateType.Avg ||
                aggregateColumn.AggregateType == AggregateType.Sum)
            {
                if (etlColumn.ETLColumnType.Type == ColumnType.Guid ||
                    etlColumn.ETLColumnType.Type == ColumnType.StringType ||
                    etlColumn.ETLColumnType.Type == ColumnType.DateTime)
                {
                    throw new AggregateFunctionMismatchType(
                        $"the aggregation function {aggregateColumn.AggregateType.ToString()} can not be used with type {etlColumn.ETLColumnType.Type}");
                }

                if (aggregateColumn.AggregateType == AggregateType.Avg)
                {
                    clonedColumn.ETLColumnType = new ETLColumnType()
                    {
                        Type = ColumnType.DoubleType,
                        Length = 15,
                        Precision = 5
                    };
                }
                else if (aggregateColumn.AggregateType == AggregateType.Sum)
                {
                    clonedColumn.ETLColumnType = new ETLColumnType()
                    {
                        Type = ColumnType.LongType
                    };
                }
            }

            clonedColumn.Name = aggregateColumn.AliasName ??
                                GetAggFunctionAliasName(aggregateColumn.ColumnName, aggregateColumn.AggregateType);
            _columnNameByColumnAliasName.Add(etlColumn.Name, clonedColumn.Name);
            resultTable.Columns.Add(clonedColumn);
        }
    }

    private static void CreateGroupByColumns(ETLTable inputTable, AggregationParameter aggregationParameter,
        ETLTable resultTable)
    {
        foreach (var groupByColumnName in aggregationParameter.GroupByColumns)
        {
            var etlColumn = inputTable.Columns.FirstOrDefault(x => x.Name == groupByColumnName);
            if (etlColumn == null)
            {
                throw new ColumnDoesNotExistException(
                    $"the column with name {groupByColumnName} does not exist in table!");
            }

            resultTable.Columns.Add(etlColumn.Clone());
        }
    }

    private string GetAggFunctionAliasName(string columnName, AggregateType aggregateType)
    {
        return new StringBuilder(aggregateType.ToString()).Append("Of").Append(columnName).ToString();
    }
}