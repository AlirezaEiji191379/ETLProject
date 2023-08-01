namespace ETLProject.Pipeline.Common.Exceptions;

public class InputPluginExceededException : Exception
{
    public InputPluginExceededException(string message) : base(message)
    {
    }
}