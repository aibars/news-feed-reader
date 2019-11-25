using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;
using NewsFeedReader.Domain.Data;
using NewsFeedReader.Providers;

namespace NewsFeedReader.Providers.Tests
{
    public class DatabaseProviderTest : IDisposable
    {
        private readonly DatabaseProvider _databaseProvider;
        private readonly ApplicationDbContext _context;

        public DatabaseProviderTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(databaseName: "temp")
                      .Options;

            _context = new ApplicationDbContext(options);

            _databaseProvider = new DatabaseProvider(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public void GetUserFeedsTest()
        {
            // Arrange
            var fakeUserFeeds = FakeData.GetUserFeeds();
            _context.Users.AddRange(FakeData.GetUsers());
            _context.UserFeeds.AddRange(fakeUserFeeds);
            _context.SaveChanges();

            // Act
            var results = _databaseProvider.GetUserFeeds("test").Result;

            // Assert
            Assert.Equal(4, results.Count);
        }

        [Fact]
        public void GetUserTest()
        {
            // Arrange
            _context.Users.AddRange(FakeData.GetUsers());
            _context.SaveChanges();
            var username = _context.Users.First().UserName;

            // Act
            var result = _databaseProvider.GetUser(username).Result;

            // Assert
            Assert.Equal(username, result.UserName);
        }

        [Fact]
        public async Task SaveFeedForUserTest()
        {
            // Arrange
            _context.Users.AddRange(FakeData.GetUsers());
            _context.SaveChanges();
            var user = _context.Users.First();
            var rss = @"https://www.feedforall.com/blog-feed.xml";
            // Act
            await _databaseProvider.SubscribeToFeed(user.UserName, rss);
            var result = _context.UserFeeds.First(x => x.UserId== user.Id && x.Url.Equals(rss));


            // Assert
            Assert.NotNull(result);
            Assert.Equal(rss, result.Url);
        }

        
    }
}
