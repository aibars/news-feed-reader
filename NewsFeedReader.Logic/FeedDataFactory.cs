using NewsFeedReader.Logic.Interface;
using System.Runtime.Caching;

namespace NewsFeedReader.Logic
{
    public static class FeedDataFactory
    {
        public static IFeedData BuildFeedData(string url)
        {
            if (MemoryCache.Default.Contains(url))
            {
                return new FeedDataCache(url);
            }
            else
            {
                return new FeedData(url);
            }
        }
    }
}