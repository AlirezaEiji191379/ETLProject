using ETLProject.Common.Table;
using System.Data;
using ETLProject.Contract.DbWriter;
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
            var query = etlTable.Query;
            var batchSize = bulkConfiguration.BatchSize;
            var limitClause = query.GetOneComponent<LimitClause>("limit");
            var hasLimit = query.GetOneComponent<LimitClause>("limit") != null;
            if (hasLimit)
            {
                batchSize = limitClause.Limit;
            }
            var dataReader = await queryFactory.FromQuery(query).PaginateAsync(1,batchSize);
            for(var i = 0; i < dataReader.TotalPages; i++)
            {
                if (i == 1 && hasLimit)
                {
                    break;
                }
                if (i != 0)
                    dataReader = await dataReader.NextAsync();
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
            }
        }

    }
}
