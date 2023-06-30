using ETLProject.Common.Database;
using System.Data;

namespace ETLProject.Common.Table
{
    public class ETLTable : IDisposable
    {
        public DataSourceType DataSourceType { get; set; }
        public TableType TableType { get; set; }
        public List<ETLColumn> Columns { get; set; }
        public string TableName { get; set; }
        public DatabaseConnectionParameters DatabaseConnection { get; set; }
        public IDbConnection DbConnection { get; set; }

        public void Dispose()
        {
            DbConnection?.Dispose();
        }
    }
}
