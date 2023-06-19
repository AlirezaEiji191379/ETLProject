namespace ETLProject.Common.Abstractions
{
    public interface IRandomStringGenerator
    {
        string GenerateRandomString(int length = 15);
    }
}
