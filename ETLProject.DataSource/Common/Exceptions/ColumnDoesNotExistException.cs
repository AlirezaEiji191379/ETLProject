namespace ETLProject.DataSource.Common.Exceptions;

public class ColumnDoesNotExistException : Exception
{
    public ColumnDoesNotExistException(string message) : base(message)
    {
    }
    
}