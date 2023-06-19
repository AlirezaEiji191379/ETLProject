using MySqlX.XDevAPI.Relational;
using Npgsql;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data;

var query = new Query("Users").Select("Id", "FullName");

using var postgresqlConnection = new NpgsqlConnection("Host=localhost;Port=5432;Database=TestDB;Username=postgres;Password=92?VH2WMrx");

var db = new QueryFactory(postgresqlConnection, new PostgresCompiler());

using var dataTable = new DataTable();

var rowsIterator = await db.FromQuery(query).PaginateAsync(1,1);

var firstRow = rowsIterator.List.First();
IDictionary<string, object> dictionaryRow = firstRow;
IEnumerable<string> columnNames = dictionaryRow.Keys;

if (rowsIterator.Page == 1)
{
    foreach (string columnName in columnNames)
    {
        var type = dictionaryRow[columnName].GetType();
        dataTable.Columns.Add(columnName, type);
    }
    foreach (var row in rowsIterator.List)
    {
        var newRow = dataTable.NewRow();
        IDictionary<string, object> dictionaryRow1 = row;
        foreach (var col in columnNames)
        {
            newRow[col] = dictionaryRow1[col];
        }
        dataTable.Rows.Add(newRow);
    }
}
while (rowsIterator.HasNext)
{
    rowsIterator = rowsIterator.Next();
    foreach (var row in rowsIterator.List)
    {
        var newRow = dataTable.NewRow();
        IDictionary<string, object> dictionaryRow1 = row;
        foreach (var col in columnNames)
        {
            newRow[col] = dictionaryRow1[col];
        }
        dataTable.Rows.Add(newRow);
    }
}

foreach(var row in dataTable.Rows)
{
    Console.WriteLine(row); 
}

