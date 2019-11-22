using NewsFeedReader.Domain.Models;
using System.Threading.Tasks;

namespace NewsFeedReader.Providers.Interface
{
    public interface IDatabaseProvider
    {
        Task<ApplicationUser> GetUser(string username);
    }
}