using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsFeedReader.Domain.Models
{
    [Table("UserFeeds")]
    public class UserFeed
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public string Url { get; set; }
    }
}
