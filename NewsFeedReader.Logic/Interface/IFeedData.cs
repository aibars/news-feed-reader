using NewsFeedReader.Logic.Models;
using System.Collections.Generic;

namespace NewsFeedReader.Logic.Interface
{
    public interface IFeedData
    {
        IEnumerable<Feed> GetFeeds();
    }
}