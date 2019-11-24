using AutoMapper;
using NewsFeedReader.Domain.ApiModels.Response;
using NewsFeedReader.Logic.Models;
using System.Collections.Generic;

namespace NewsFeedReader.Web.Mappings
{
    public class FeedToFeedModelConverter : ITypeConverter<List<Feed>, FeedModel>
    {
        public FeedModel Convert(List<Feed> source, FeedModel destination, ResolutionContext context)
        {
            var feedModel = new FeedModel()
            {
                Items = new List<FeedItem>()
            };

            source.ForEach(
                y =>
                {
                    var item = new FeedItem()
                    {
                        Title = y.Title,
                        Link = y.Link,
                        PublishDate = y.PublishDate,
                        Content = y.Content,
                    };

                    feedModel.Items.Add(item);
                });

            return feedModel;
        }
    }
}
