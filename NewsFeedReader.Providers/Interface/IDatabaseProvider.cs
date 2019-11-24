using NewsFeedReader.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsFeedReader.Providers.Interface
{
    public interface IDatabaseProvider
    {
        Task<ApplicationUser> GetUser(string username);

        Task SubscribeToFeed(string username, string url);

        Task<List<UserFeed>> GetUserFeeds(string username);
    }
}