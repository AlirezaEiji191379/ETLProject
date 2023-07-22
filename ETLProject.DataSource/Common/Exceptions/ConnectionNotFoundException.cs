namespace ETLProject.DataSource.Common.Exceptions;

public class ConnectionNotFoundException : Exception
{
    public ConnectionNotFoundException(string connectionId) : base(
        $"connection with id {connectionId} does not exist in database")
    {
    }
}