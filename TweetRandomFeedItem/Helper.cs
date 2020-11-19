using System;
using System.Text.RegularExpressions;

namespace TweetRandomFeedItem
{
    public class Helper
    {
        static readonly Regex invalidChars = new Regex("[- ]");
  
        public static string SanitizeTagName(string tag)
        {
            return $"#{invalidChars.Replace(tag, "").Replace("#", "sharp").Replace(".", "dot")}";
        }

        public static T GetEnv<T>(string environmentVariable)
        {
            try
            {
                return (T)Convert.ChangeType(Environment.GetEnvironmentVariable(environmentVariable), typeof(T));
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine($"The environment variable {environmentVariable} is required and missing.");
                throw;
            }
        }

        public static T GetEnv<T>(string environmentVariable, string defaultValue)
        {
            return (T)Convert.ChangeType(Environment.GetEnvironmentVariable(environmentVariable) ?? defaultValue, typeof(T));
        }
    }
}
