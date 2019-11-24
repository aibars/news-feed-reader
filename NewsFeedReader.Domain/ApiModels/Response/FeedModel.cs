using System;
using System.Collections.Generic;

namespace NewsFeedReader.Domain.ApiModels.Response
{
    public class FeedModel
    {
        public List<FeedItem> Items { get; set; }
        public int TotalCount { get; set; }
    }

    public class FeedItem
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? PublishDate { get; set; }
    }
}