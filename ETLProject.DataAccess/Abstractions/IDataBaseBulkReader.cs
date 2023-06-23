using ETLProject.Common.Table;
using System.Data;

namespace ETLProject.DataSource.Query.Abstractions
{
    public interface IDataBaseBulkReader
    {
        DataTable ReadDataInBulk(ETLTable etlTable);
    }
}
