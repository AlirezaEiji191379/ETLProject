using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.DataSource.Abstractions;
using System.Data;
using System.Data.SqlClient;

namespace ETLProject.DataSource.DataSourceInserting
{
    internal class SqlServerBulkCopy : IDataBulkInserter
    {
        public DataSourceType DataSourceType => DataSourceType.SQLServer;

        public async Task InsertBulk(DataTable dataTable, ETLTable etlTable)
        {
            using var sqlBulkCopy = new SqlBulkCopy(etlTable.DbConnection as SqlConnection);
            sqlBulkCopy.DestinationTableName = etlTable.TableName;
            foreach (var column in etlTable.Columns)
                sqlBulkCopy.ColumnMappings.Add(column.Name,column.Name);
            await sqlBulkCopy.WriteToServerAsync(dataTable);

        }
    }
}
