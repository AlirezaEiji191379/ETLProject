using ETLProject.Common.Abstractions;
using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.DataSource.Abstractions;
using SqlKata;
using SqlKata.Contract.CreateTable;

namespace ETLProject.DataSource.TableFactory
{
    internal class DbTempTableCreator : IDbTableFactory
    {
        private readonly IRandomStringGenerator _randomStringGenerator;
        private readonly IColumnMapperProvider _columnMapperProvider;
        private readonly IQueryFactoryProvider _queryFactoryProvider;
        public DbTempTableCreator(IRandomStringGenerator randomStringGenerator,
            IQueryFactoryProvider queryFactoryProvider,
            IColumnMapperProvider columnMapperProvider) 
        {
            _randomStringGenerator = randomStringGenerator;
            _columnMapperProvider = columnMapperProvider;
            _queryFactoryProvider = queryFactoryProvider;
        }

        public async Task CreateTempTable(ETLTable etlTable)
        {
            var columnMapper = _columnMapperProvider.GetColumnTypeMapper(etlTable.DatabaseConnection.DataSourceType);
            etlTable.TableName =  "ETL_" + _randomStringGenerator.GenerateRandomString();
            if (etlTable.DataSourceType == DataSourceType.SQLServer)
                etlTable.TableName = "#" + etlTable.TableName;
            var tableDefinition = new List<TableColumnDefinitionDto>();
            foreach(var etlColumn in etlTable.Columns)
            {
                tableDefinition.Add(new TableColumnDefinitionDto()
                {
                    ColumnName = etlColumn.Name,
                    ColumnDbType = columnMapper.AdaptType(etlColumn),
                    IsAutoIncrement = false,
                    IsIdentity= false,
                    IsPrimaryKey=false,
                    IsNullable=true,
                    IsUnique=false,
                });
            }
            var query = new Query(etlTable.TableName).CreateTable(tableDefinition,SqlKata.Contract.CreateTable.TableType.Temporary);
            var queryFactory = _queryFactoryProvider.GetQueryFactory(etlTable);
            await queryFactory.ExecuteAsync(query);
        }
    }
}
