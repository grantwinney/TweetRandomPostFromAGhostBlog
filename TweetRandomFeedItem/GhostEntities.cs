using System.Collections.Generic;

namespace TweetRandomFeedItem
{
    public class PostResponse
    {
        public List<Post> Posts { get; set; }
        public Meta Meta { get; set; }
    }

    public class Post
    {
        public string Id { get; set; }
        public string Uuid { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Slug { get; set; }
        public string MobileDoc { get; set; }
        public string Html { get; set; }
        public string PlainText { get; set; }
        public string FeatureImage { get; set; }
        public bool Featured { get; set; }
        public bool Page { get; set; }
        public string Status { get; set; }
        public string Locale { get; set; }
        public string Visibility { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string PublishedAt { get; set; }
        public string PublishedBy { get; set; }
        public string CustomExcerpt { get; set; }
        public string CodeInjectionHead { get; set; }
        public string CodeInjectionFoot { get; set; }
        public string OgImage { get; set; }
        public string OgTitle { get; set; }
        public string OgDescription { get; set; }
        public string TwitterImage { get; set; }
        public string TwitterTitle { get; set; }
        public string TwitterDescription { get; set; }
        public string CustomTemplate { get; set; }
        public List<Tag> Tags { get; set; }
        public Tag PrimaryTag { get; set; }
        public string Url { get; set; }
        public string CommentId { get; set; }
    }

    public class Tag
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string FeatureImage { get; set; }
        public string Visibility { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string Parent { get; set; }
    }

    public class Meta
    {
        public Pagination Pagination { get; set; }
    }

    public class Pagination
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        public int Pages { get; set; }
        public int Total { get; set; }
        public int Next { get; set; }
        public int Prev { get; set; }
    }

    public class AuthToken
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
        public string TokenType { get; set; }
    }
}
