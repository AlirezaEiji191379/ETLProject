using ETLProject.Common.Table;
using ETLProject.DataSource.Query.Abstractions;
using ETLProject.DataSource.QueryManager.Common;
using System.Data;

namespace ETLProject.DataSource.QueryManager.DataSourceReading
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


        public DataTable ReadDataInBulk(ETLTable etlTable, BulkConfiguration bulkConfiguration)
        {
            using var queryFactory = _queryFactoryProvider.GetQueryFactory(etlTable);
            var tableName = _tableNameProvider.GetTableName(etlTable);
            var query = new SqlKata.Query(tableName);



            return null;
        }





        public void Dispose()
        {

        }
    }
}
