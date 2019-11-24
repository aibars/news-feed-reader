using NewsFeedReader.Logic.Interface;
using NewsFeedReader.Logic.Models;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace NewsFeedReader.Logic
{
    public class FeedDataCache : IFeedData
    {
        private readonly string key;

        public FeedDataCache(string key)
        {
            this.key = key;
        }

        public IEnumerable<Feed> GetFeeds()
        {
            var feeds = new List<Feed>();
            object cachedValue = MemoryCache.Default.Get(this.key);
            var list = cachedValue as List<Feed>;
            if (list != null)
            {
                feeds = list;
            }

            return feeds;
        }
    }
}
