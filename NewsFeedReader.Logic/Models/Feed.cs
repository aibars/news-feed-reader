using System;

namespace NewsFeedReader.Logic.Models
{
    public class Feed
    {
        public string Link { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; internal set; }
    }
}