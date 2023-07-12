namespace ETLProject.DataSource.QueryBusiness.SortBusiness.Exceptions;

public class ColumnDoesNotExistException : Exception
{
    public ColumnDoesNotExistException(string columnName) : base(
        $"column with name {columnName} does not exist in table")
    {
    }
}