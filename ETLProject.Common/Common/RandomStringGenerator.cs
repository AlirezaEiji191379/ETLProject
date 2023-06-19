using ETLProject.Common.Abstractions;
using System.Text;

namespace ETLProject.Common.Common
{
    public class RandomStringGenerator : IRandomStringGenerator
    {
        public string GenerateRandomString(int length = 15)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                stringBuilder.Append(chars[index]);
            }
            return stringBuilder.ToString();
        }
    }
}
