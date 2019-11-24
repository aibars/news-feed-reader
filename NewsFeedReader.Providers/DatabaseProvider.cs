using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NewsFeedReader.Domain.Data;
using NewsFeedReader.Domain.Models;
using NewsFeedReader.Providers.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsFeedReader.Providers
{
    /// <summary>
    /// Methods for accessing the database
    /// </summary>
    public class DatabaseProvider : IDatabaseProvider
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IConfiguration _configuration;

        public DatabaseProvider(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Obtains a user by username
        /// </summary>
        public async Task<ApplicationUser> GetUser(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<List<UserFeed>> GetUserFeeds(string username)
        {
            return await _context.UserFeeds.Where(x => x.User.UserName.Equals(username)).ToListAsync();
        }

        /// <summary>
        /// Obtains a user by username
        /// </summary>
        public async Task SubscribeToFeed(string username, string url)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.UserName == username);

            _context.UserFeeds.Add(new UserFeed
            {
                Url = url,
                UserId = user.Id
            });

            await _context.SaveChangesAsync();
        }
    }
}
