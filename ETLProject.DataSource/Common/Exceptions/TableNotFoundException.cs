namespace ETLProject.DataSource.Common.Exceptions;

public class TableNotFoundException : Exception
{
    public TableNotFoundException(string tableName) : base(
        $"the table with name {tableName} does not exist in the destination datasource")
    {
    }
}