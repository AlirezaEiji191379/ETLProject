using ETLProject.Common.Database;
using ETLProject.Common.Table;
using ETLProject.DataSource.Abstractions;
using Npgsql;
using System.Data;

namespace ETLProject.DataSource.DataSourceInserting
{
    internal class PostgresqlBulkCopy : IDataBulkInserter
    {
        public DataSourceType DataSourceType => DataSourceType.Postgresql;

        public async Task InsertBulk(DataTable dataTable, ETLTable etlTable)
        {
            //TODO : refactor query making
            var columnSet = string.Format($"({string.Join(",",etlTable.Columns.Select(x => x.Name))})");
            var importQueryString = $"COPY \"{etlTable.TableName}\" {columnSet} FROM STDIN (FORMAT BINARY)";
            using var writer = (etlTable.DbConnection as NpgsqlConnection).BeginBinaryImport(importQueryString);
            foreach (DataRow row in dataTable.Rows)
            {
                await writer.StartRowAsync();
                for(var i = 0;i< etlTable.Columns.Count; i++)
                {
                    var column = etlTable.Columns[i];
                    switch (column.ETLColumnType.Type)
                    {
                        case ColumnType.StringType:
                            await writer.WriteAsync(row[column.Name],NpgsqlTypes.NpgsqlDbType.Varchar).ConfigureAwait(false);
                            break;
                        case ColumnType.Int32Type:
                            await writer.WriteAsync(row[column.Name],NpgsqlTypes.NpgsqlDbType.Integer).ConfigureAwait(false);
                            break;
                        case ColumnType.DoubleType:
                            await writer.WriteAsync(row[column.Name],NpgsqlTypes.NpgsqlDbType.Double).ConfigureAwait(false);
                            break;
                        case ColumnType.LongType:
                            await writer.WriteAsync(row[column.Name],NpgsqlTypes.NpgsqlDbType.Bigint).ConfigureAwait(false);
                            break;
                        case ColumnType.BooleanType:
                            await writer.WriteAsync(row[column.Name],NpgsqlTypes.NpgsqlDbType.Boolean).ConfigureAwait(false);
                            break;
                        default:
                            throw new NotSupportedException();
                    }
                }
            }
            await writer.CompleteAsync();
        }
    }
}
