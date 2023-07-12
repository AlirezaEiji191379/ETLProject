using ETLProject.Common.Database;
using System.Data;
using System.Text;
using SqlKata;

namespace ETLProject.Common.Table
{
    public class ETLTable : IDisposable
    {
        private Query? query;
        
        public DataSourceType DataSourceType { get; set; }
        public TableType TableType { get; set; }
        public List<ETLColumn> Columns { get; set; }
        public string TableName { get; set; }
        public string AliasName { get; set; }
        public string TableSchema { get; set; }
        public string TableFullName
        {
            get
            {
                var tableFullNameBuilder = new StringBuilder(TableSchema).Append(".").Append(TableName);
                if (AliasName == null)
                    return tableFullNameBuilder.ToString();
                return tableFullNameBuilder.Append(" as ").Append(AliasName).ToString();
            }
        }
        public DatabaseConnectionParameters DatabaseConnection { get; set; }
        public IDbConnection DbConnection { get; set; }

        public Query Query
        {
            get
            {
                if (query != null) return query;
                query = new Query(TableFullName);
                var columnNameList = Columns.Select(etlColumn => GetColumnFullNameById(etlColumn.EtlColumnId));
                query.Select(columnNameList);
                return query;
            }
            set => query = value;
        }

        public string GetColumnFullNameById(Guid etlColumnId)
        {
            var column = Columns.Where(column => column.EtlColumnId == etlColumnId).First();
            if (AliasName == null)
                return column.Name;
            return new StringBuilder(AliasName).Append(".").Append(column.Name).ToString();
        }

        public List<ETLColumn> CloneEtlColumns()
        {
            var result = new List<ETLColumn>();
            foreach (var column in Columns)
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
