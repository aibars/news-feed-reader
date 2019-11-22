using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsFeedReader.Providers.Interface;
using System.Threading.Tasks;

namespace NewsFeedReader.Web
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FeedController : ControllerBase
    {
        protected readonly IDatabaseProvider _databaseProvider;
        protected readonly IMapper _mapper;

        public FeedController(IDatabaseProvider databaseProvider, IMapper mapper)
        {
            _databaseProvider = databaseProvider;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtains all messages from the database
        /// </summary>
        public async Task<string> GetFeedItems()
        {
            return "";
        }
    }
}