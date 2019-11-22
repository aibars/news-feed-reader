using System.ComponentModel.DataAnnotations;

namespace NewsFeedReader.Domain.ApiModels.Request
{
    public class LoginRequestDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}