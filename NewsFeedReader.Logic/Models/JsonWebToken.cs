using System;

namespace NewsFeedReader.Logic.Models
{
    public class JsonWebToken
    {
        public string Token { get; protected set; }
        public long Expiration { get; protected set; }

        public string UserName { get; set; }

        public JsonWebToken(string username, string token, long expiration)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Invalid token.");

            if (expiration <= 0)
                throw new ArgumentException("Invalid expiration.");

            Token = token;
            Expiration = expiration;
            UserName = username;
        }

        public bool IsExpired() => DateTime.UtcNow.Ticks > Expiration;
    }
}