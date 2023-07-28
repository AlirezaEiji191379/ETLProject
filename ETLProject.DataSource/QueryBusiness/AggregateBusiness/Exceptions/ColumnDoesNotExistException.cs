namespace ETLProject.DataSource.QueryBusiness.AggregateBusiness.Exceptions;

public class ColumnDoesNotExistException : Exception
{
    public ColumnDoesNotExistException(string message) : base(message)
    {
    }
    
}