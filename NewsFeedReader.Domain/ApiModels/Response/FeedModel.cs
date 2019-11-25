using System;

namespace NewsFeedReader.Domain.ApiModels.Response
{
    public class FeedModel
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? PublishDate { get; set; }
    }
}