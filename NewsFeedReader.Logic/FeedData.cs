using NewsFeedReader.Logic.Interface;
using NewsFeedReader.Logic.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace NewsFeedReader.Logic
{
    public class FeedData : IFeedData
    {
        public IEnumerable<Feed> GetFeeds(string url, FeedType format)
        {
            List<Feed> parsedValues = new List<Feed>();

            if (format.Equals("RSS"))
            {
                parsedValues = FeedParser.Parse(url, FeedType.RSS);
            }
            else if (format.Equals("ATOM"))
            {
                parsedValues = FeedParser.Parse(url, FeedType.Atom);
            }


            CacheItemPolicy policy = new CacheItemPolicy
            {
                SlidingExpiration = new TimeSpan(0, 0, 0, 120)
            };

            MemoryCache.Default.Add(url, parsedValues, policy);

            return parsedValues;
        }
    }
}
