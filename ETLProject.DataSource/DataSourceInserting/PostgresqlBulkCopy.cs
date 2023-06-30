using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.DataSource.Abstractions;
using System.Data;

namespace ETLProject.DataSource.DataSourceInserting
{
    internal class PostgresqlBulkCopy : IDataBulkInserter
    {
        public DataSourceType DataSourceType => DataSourceType.Postgresql;

        public ETLTable InsertBulk(DataTable dataTable, ETLTable etlTable)
        {
            throw new NotImplementedException();
        }
    }
}
