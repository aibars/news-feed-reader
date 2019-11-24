using NewsFeedReader.Logic.Interface;
using NewsFeedReader.Logic.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace NewsFeedReader.Logic
{
    public class FeedData : IFeedData
    {
        private string url;

        public FeedData(string url)
        {
            this.url = url;
        }

        public IEnumerable<Feed> GetFeeds()
        {
            var parsedValues = FeedParser.Parse(url);

            CacheItemPolicy policy = new CacheItemPolicy
            {
                SlidingExpiration = new TimeSpan(0, 0, 0, 120)
            };

            MemoryCache.Default.Add(url, parsedValues, policy);

            return parsedValues;
        }
    }
}