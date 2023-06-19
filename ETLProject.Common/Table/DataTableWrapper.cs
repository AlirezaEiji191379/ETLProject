using ETLProject.Common.Database;
using System.Data;

namespace ETLProject.Common.Table
{
    public class DataTableWrapper
    {
        public DataSourceType DataSourceType { get; set; }
        public TableType TableType { get; set; }
        public DataTable DataTable { get; set; }

    }
}
