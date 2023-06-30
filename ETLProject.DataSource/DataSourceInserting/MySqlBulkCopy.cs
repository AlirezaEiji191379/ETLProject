using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.DataSource.Abstractions;
using System.Data;

namespace ETLProject.DataSource.DataSourceInserting
{
    internal class MySqlBulkCopy : IDataBulkInserter
    {
        public DataSourceType DataSourceType => DataSourceType.MySql;

        public Task InsertBulk(DataTable dataTable, ETLTable etlTable)
        {
            throw new NotImplementedException();
        }
    }
}
