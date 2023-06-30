using ETLProject.Common.Table;
using System.Data;

namespace ETLProject.DataSource.Abstractions
{
    public interface IDataBulkInserter
    {
        ETLTable InsertBulk(DataTable dataTable,ETLTable etlTable);
    }
}
