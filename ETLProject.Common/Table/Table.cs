using ETLProject.Common.Database;

namespace ETLProject.Common.Table
{
    public class Table
    {
        public DataSourceType DataSourceType { get; set; }
        public TableType TableType { get; set; }
        public List<Column> Columns { get; set; }
    }
}
