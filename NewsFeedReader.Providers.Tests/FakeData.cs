using GenFu;
using NewsFeedReader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsFeedReader.Providers.Tests
{
    public static class FakeData
    {
        private static Guid Guid = Guid.NewGuid();
        public static List<UserFeed> GetUserFeeds()
        {
            var rnd = new Random();
            var userFeeds = A.ListOf<UserFeed>(4);
            var user = new ApplicationUser { Id = Guid };
            userFeeds.ForEach(y => { y.Id = Guid.NewGuid(); y.UserId = user.Id; y.Url = "new string";  });
            return userFeeds.Select(_ => _).ToList();
        }

        public static List<ApplicationUser> GetUsers()
        {
            var users = A.ListOf<ApplicationUser>(5);
            users.ForEach(y =>  y.Id = Guid.NewGuid());
            users[0].Id = Guid;
            users[0].UserName = "test";
            return users.Select(_ => _).ToList();
        }
    }
}
