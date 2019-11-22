using NewsFeedReader.Domain.Models;
using NewsFeedReader.Logic.Models;

namespace NewsFeedReader.Logic.Interface
{
    public interface ITokenService
    {
        JsonWebToken GenerateJwtToken(ApplicationUser user);
    }
}