using ETLProject.Common.Database;
using ETLProject.Common.Table;
using System.Data;

namespace ETLProject.DataSource.Abstractions
{
    public interface IDataBulkInserter
    {
        DataSourceType DataSourceType { get; }
        ETLTable InsertBulk(DataTable dataTable,ETLTable etlTable);
    }
}
