using ETLProject.Common.Table;
using ETLProject.DataSource.Common;
using System.Data;
using ETLProject.Contract.DbWriter;

namespace ETLProject.DataSource.Abstractions
{
    public interface IDataBaseBulkReader
    {
        IAsyncEnumerable<DataTable> ReadDataInBulk(ETLTable etlTable, BulkConfiguration bulk);
    }
}
