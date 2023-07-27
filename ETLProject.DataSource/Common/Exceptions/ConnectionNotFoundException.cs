namespace ETLProject.DataSource.Common.Exceptions;

public class ConnectionNotFoundException : Exception
{
    public ConnectionNotFoundException() : base(
        $"connection does not exist in database")
    {
    }
}