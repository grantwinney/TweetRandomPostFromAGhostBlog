using System;
using System.Linq;
using System.Text.RegularExpressions;
using RestSharp;
using Tweetinvi;

namespace TweetRandomFeedItem
{
    class Program
    {
        const string API_URL = "/ghost/api/v0.1";
        const string DEFAULT_MAX_TAG_COUNT = "3";
        const string DEFAULT_POST_RETRIEVAL_LIMIT = "9999999";
        const int TWITTER_URL_SIZE = 23;
        const int MAX_TWEET_LENGTH = 280;
        static string accessToken;

        static Random rnd = new Random();

        public static void Main()
        {
            var message = CreateMessage(GetPost(GetRandomPostId()));

            SendTweet(message);

            Console.WriteLine($"Tweeted message:\r\n\r\n{message}");
        }

        static string GetRandomPostId()
        {
            var ghostUri = Helper.GetEnv("GHOST_URI");
            var postLimit = Helper.GetEnv("GHOST_POST_RETRIEVAL_LIMIT", DEFAULT_POST_RETRIEVAL_LIMIT);
          
            var client = new RestClient { BaseUrl = new Uri($"{ghostUri}{API_URL}") };

            var requestAllPostIds = new RestRequest("posts", Method.GET);
            AttachAuthToRequest(requestAllPostIds);

            requestAllPostIds.AddQueryParameter("limit", postLimit);
            requestAllPostIds.AddQueryParameter("fields", "id");

            var posts = client.Execute<PostResponse>(requestAllPostIds).Data.Posts;
           
            return posts[rnd.Next(0, posts.Count - 1)].Id;
        }

        static Post GetPost(string postId)
        {
            var ghostUri = Helper.GetEnv("GHOST_URI");
          
            var client = new RestClient { BaseUrl = new Uri($"{ghostUri}{API_URL}") };

            var request = new RestRequest($"posts/{postId}", Method.GET);
            AttachAuthToRequest(request);

            request.AddQueryParameter("include", "tags");

            return client.Execute<PostResponse>(request).Data.Posts.Single();
        }

        static string CreateMessage(Post post)
        {
            var ghostUri = Helper.GetEnv("GHOST_URI");
            var maxTagCount = Convert.ToInt32(Helper.GetEnv("TWITTER_MAX_TAG_COUNT", DEFAULT_MAX_TAG_COUNT));

            var remainingSpaceForTags = MAX_TWEET_LENGTH - (post.Title.Length + 1 + TWITTER_URL_SIZE);

            var pattern = new Regex("[- ]");
            var tags = post.Tags.Take(maxTagCount).Select(t => $"#{pattern.Replace(t.Name, "").Replace("#", "sharp").Replace(".", "dot")}");
          
            var tagsFlat = "";
            foreach (var t in tags)
            {
                if ($"{tagsFlat} {t}".Length > remainingSpaceForTags)
                    break;
                
                tagsFlat += $" {t}";
            }

            return $"{post.Title}{tagsFlat}\r\n{ghostUri}{post.Url}";
        }

        static void SendTweet(string message)
        {
            var consumerKey = Helper.GetEnv("TWITTER_CONSUMER_KEY");
            var consumerSecret = Helper.GetEnv("TWITTER_CONSUMER_SECRET");
            var userAccessToken = Helper.GetEnv("TWITTER_USER_ACCESS_TOKEN");
            var userAccessTokenSecret = Helper.GetEnv("TWITTER_USER_ACCESS_TOKEN_SECRET");

            Auth.SetUserCredentials(consumerKey, consumerSecret, userAccessToken, userAccessTokenSecret);

            Tweet.PublishTweet(message);
        }

        static void AttachAuthToRequest(RestRequest request)
        {
            var ghostUri = Helper.GetEnv("GHOST_URI");
            var ghostClientId = Helper.GetEnv("GHOST_CLIENT_ID");
            var ghostClientSecret = Helper.GetEnv("GHOST_CLIENT_SECRET");
            var ghostUsername = Helper.GetEnv("GHOST_USERNAME");
            var ghostPassword = Helper.GetEnv("GHOST_PASSWORD");

            if (!String.IsNullOrWhiteSpace(ghostUsername) && !String.IsNullOrWhiteSpace(ghostPassword))
            {
                if (accessToken == null)
                {
                    var authRequest = new RestRequest("authentication/token", Method.POST);

                    authRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    authRequest.AddHeader("Accept", "application/json");

                    authRequest.AddParameter("grant_type", "password");
                    authRequest.AddParameter("client_id", ghostClientId);
                    authRequest.AddParameter("client_secret", ghostClientSecret);
                    authRequest.AddParameter("username", ghostUsername);
                    authRequest.AddParameter("password", ghostPassword);

                    var client = new RestClient { BaseUrl = new Uri($"{ghostUri}{API_URL}") };

                    accessToken = client.Execute<AuthToken>(authRequest).Data.AccessToken;
                }

                request.AddParameter("Authorization", $"Bearer {accessToken}", ParameterType.HttpHeader);
            }
            else
            {
                request.AddQueryParameter("client_id", ghostClientId);
                request.AddQueryParameter("client_secret", ghostClientSecret);
            }
        }
    }
}
