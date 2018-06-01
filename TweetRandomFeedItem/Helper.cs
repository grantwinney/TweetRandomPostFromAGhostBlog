using System;
namespace TweetRandomFeedItem
{
    public class Helper
    {
        public static string GetEnv(string environmentVariable)
        {
            return Environment.GetEnvironmentVariable(environmentVariable);
        }


        public static string GetEnv(string environmentVariable, string defaultValue)
        {
            var env = GetEnv(environmentVariable);

            return !String.IsNullOrWhiteSpace(env) ? env : defaultValue;
        }
    }
}
