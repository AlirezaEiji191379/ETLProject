using ETLProject.Common.Table;
using ETLProject.DataSource.Query.Abstractions;
using System.Data;

namespace ETLProject.DataSource.Query.DataSourceInserting
{
    internal class DataBaseBulkReader : IDataBaseBulkReader
    {
        public DataTable ReadDataInBulk(ETLTable etlTable)
        {
            throw new NotImplementedException();
        }
    }
}
