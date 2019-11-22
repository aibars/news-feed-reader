using Microsoft.AspNetCore.Identity;
using System;

namespace NewsFeedReader.Domain.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public DateTime? LastLoginDate { get; set; }
    }
}