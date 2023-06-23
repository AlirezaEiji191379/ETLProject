using ETLProject.Common.Table;
using System.Data;
using SqlKata;
using ETLProject.DataSource.Common;
using ETLProject.DataSource.Abstractions;

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


        public DataTable ReadDataInBulk(ETLTable etlTable, BulkConfiguration bulkConfiguration)
        {
            using var queryFactory = _queryFactoryProvider.GetQueryFactory(etlTable);
            var tableName = _tableNameProvider.GetTableName(etlTable);
            var query = new Query(tableName);



            return null;
        }





        public void Dispose()
        {

        }
    }
}
