using Entities.User;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IJwtService
    {
        Task<AccessToken> GenerateAsync(User user);
    }
}