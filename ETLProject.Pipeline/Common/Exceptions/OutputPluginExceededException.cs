namespace ETLProject.Pipeline.Common.Exceptions;

public class OutputPluginExceededException : Exception
{
    public OutputPluginExceededException(string message) : base(message)
    {
    }
}