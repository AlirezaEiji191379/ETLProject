using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.DataSource.Abstractions;
using System.Data;

namespace ETLProject.DataSource.DataSourceInserting
{
    internal class SqlServerBulkCopy : IDataBulkInserter
    {
        public DataSourceType DataSourceType => DataSourceType.SQLServer;

        public ETLTable InsertBulk(DataTable dataTable, ETLTable etlTable)
        {
            return null;
        }
    }
}
