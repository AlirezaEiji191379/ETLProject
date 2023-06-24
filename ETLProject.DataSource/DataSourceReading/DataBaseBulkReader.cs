using ETLProject.Common.Table;
using System.Data;
using SqlKata;
using ETLProject.DataSource.Common;
using ETLProject.DataSource.Abstractions;
using SqlKata.Execution;

namespace ETLProject.DataSource.DataSourceReading
{
    internal class DataBaseBulkReader : IDataBaseBulkReader
    {

        private readonly IQueryFactoryProvider _queryFactoryProvider;
        private readonly ITableNameProvider _tableNameProvider;

        public DataBaseBulkReader(IQueryFactoryProvider queryFactoryProvider, ITableNameProvider tableNameProvider)
        {
            _queryFactoryProvider = queryFactoryProvider;
            _tableNameProvider = tableNameProvider;
        }


        public async IAsyncEnumerable<DataTable> ReadDataInBulk(ETLTable etlTable, BulkConfiguration bulkConfiguration)
        {
            using var queryFactory = _queryFactoryProvider.GetQueryFactory(etlTable);
            var tableName = _tableNameProvider.GetTableName(etlTable);
            var columnStrings = etlTable.Columns.Select(x => x.Name);
            var query = new Query(tableName).Select(columnStrings);
            var dataReader = await queryFactory.FromQuery(query).PaginateAsync(1,bulkConfiguration.BatchSize);
            for(var i =0; i < dataReader.Count; i++)
            {
                var dataTable = new DataTable();
                IDictionary<string, object> schemaRow = dataReader.List.First();
                var columnNames = schemaRow.Keys;
                columnNames.ToList().ForEach(columnName =>
                {
                    dataTable.Columns.Add(columnName, schemaRow[columnName].GetType());
                });
                dataReader.List.ToList().ForEach(row =>
                {
                    dataTable.Rows.Add(((IDictionary<string, object>)row).Values.ToArray());
                });
                yield return dataTable;
                dataReader = dataReader.Next();
            }
        }





        public void Dispose()
        {

        }
    }
}
