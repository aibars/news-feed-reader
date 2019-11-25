using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsFeedReader.Domain.ApiModels.Request;
using NewsFeedReader.Domain.ApiModels.Response;
using NewsFeedReader.Logic;
using NewsFeedReader.Logic.Models;
using NewsFeedReader.Providers.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsFeedReader.Web
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FeedsController : ControllerBase
    {
        protected readonly IDatabaseProvider _databaseProvider;
        protected readonly IMapper _mapper;

        public FeedsController(IDatabaseProvider databaseProvider, IMapper mapper)
        {
            _databaseProvider = databaseProvider;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("subscribe")]
        public async Task<IActionResult> Subscribe([FromBody]SubscribeFeedModel model)
        {
            await _databaseProvider.SubscribeToFeed(User.Identity.Name, model.Url);
            return Ok();
        }

        /// <summary>
        /// Obtains all feeds a particular user
        /// </summary>
        [Route("user")]
        public async Task<List<FeedModel>> GetFeedItems()
        {
            var model = new FeedModel();

            var userFeeds = await _databaseProvider.GetUserFeeds(User.Identity.Name);
            var feeds =  new List<Feed>();
            foreach (var feed in userFeeds)
            {
                var feedData = FeedDataFactory.BuildFeedData(feed.Url);
                var allResults = feedData.GetFeeds();

                var serviceCount = allResults.Count();

                var results = allResults
                        .OrderByDescending(x => x.PublishDate)
                        .Take(10)
                        .ToList();

                feeds.AddRange(results);
            }

            feeds.OrderByDescending(x => x.PublishDate);
            return _mapper.Map<List<FeedModel>>(feeds);
        }
    }
}