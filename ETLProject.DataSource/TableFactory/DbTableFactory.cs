using ETLProject.Common.Abstractions;
using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.DataSource.Abstractions;
using SqlKata;
using SqlKata.Contract.CreateTable;

namespace ETLProject.DataSource.TableFactory
{
    internal class DbTableFactory : IDbTableFactory
    {
        private readonly IRandomStringGenerator _randomStringGenerator;
        private readonly IColumnMapperProvider _columnMapperProvider;
        private readonly IQueryFactoryProvider _queryFactoryProvider;

        public DbTableFactory(IRandomStringGenerator randomStringGenerator,
            IQueryFactoryProvider queryFactoryProvider,
            IColumnMapperProvider columnMapperProvider)
        {
            _randomStringGenerator = randomStringGenerator;
            _columnMapperProvider = columnMapperProvider;
            _queryFactoryProvider = queryFactoryProvider;
        }

        public async Task CreateTable(ETLTable etlTable)
        {
            //SetTableName(etlTable);
            SqlKata.Contract.CreateTable.TableType tableType = GetTableType(etlTable.TableType);
            var columnMapper = _columnMapperProvider.GetColumnTypeMapper(etlTable.DataSourceType);
            var tableDefinition = etlTable.Columns.Select(etlColumn => new TableColumnDefinitionDto
            {
                ColumnName = etlColumn.Name,
                ColumnDbType = columnMapper.AdaptType(etlColumn.ETLColumnType),
                IsAutoIncrement = etlColumn.EtlColumnParameters.IsAutoIncrement,
                IsIdentity = etlColumn.EtlColumnParameters.IsIdentity,
                IsPrimaryKey = etlColumn.EtlColumnParameters.IsPrimaryKey,
                IsNullable = etlColumn.EtlColumnParameters.IsNullable,
                IsUnique = etlColumn.EtlColumnParameters.IsUnique,
            }).ToList();

            var query = new Query(etlTable.TableName).CreateTable(tableDefinition, tableType);
            await ExecuteDDLQuery(etlTable, query);
        }


        public async Task CreateTableAs(ETLTable etlTable, string newTableName,
            ETLProject.Common.Table.TableType newTableType)
        {
            SqlKata.Contract.CreateTable.TableType tableType = GetTableType(newTableType);
            var query = new Query(newTableName).CreateTableAs(etlTable.Query, tableType);
            await ExecuteDDLQuery(etlTable, query);
        }

        public async Task SelectInto(ETLTable etlTable, string newTableName)
        {
            var etlTableColumnNameList = etlTable.Columns.Select(column => column.Name).ToList();
            var selectIntoQuery = new Query().From(etlTable.Query, _randomStringGenerator.GenerateRandomString(10))
                .Select(etlTableColumnNameList).Into(newTableName);
            await ExecuteDDLQuery(etlTable, selectIntoQuery);
        }

        private async Task ExecuteDDLQuery(ETLTable etlTable, Query query)
        {
            var queryFactory = _queryFactoryProvider.GetQueryFactory(etlTable);
            await queryFactory.ExecuteAsync(query);
        }

        private static SqlKata.Contract.CreateTable.TableType GetTableType(ETLProject.Common.Table.TableType tableType)
        {
            return tableType == ETLProject.Common.Table.TableType.Temp
                ? SqlKata.Contract.CreateTable.TableType.Temporary
                : SqlKata.Contract.CreateTable.TableType.Permanent;
        }
    }
}