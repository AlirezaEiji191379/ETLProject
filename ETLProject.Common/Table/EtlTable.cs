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

        public List<ETLColumn> CloneEtlColumns()
        {
            var result = new List<ETLColumn>();
            foreach(var column in Columns)
            {
                result.Add(column.Clone());
            }
            return result;
        }


        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    DbConnection?.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
