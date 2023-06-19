using ETLProject.Common.Database;
using ETLProject.Common.Database.DBConnection;

namespace ETLProject.Common.Table
{
    public class ETLTable
    {
        public DataSourceType DataSourceType { get; set; }
        public TableType TableType { get; set; }
        public List<Column> Columns { get; set; }
        public string TableName { get; set; }
        public DatabaseConnection DatabaseConnection { get; set; }
    }
}
