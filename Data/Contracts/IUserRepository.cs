using System.Threading;
using System.Threading.Tasks;
using Entities.User;

namespace Data.Contracts
{
    public interface IUserRepository:IGenericRepository<User>
    {
        Task<User> GetByUserAndPassAsync(string Username, string password, CancellationToken cancellationToken);
        Task AddAsync(User user, string Password, CancellationToken cancellationToken);
        Task UpdateSecurityStampAsync(User user, CancellationToken cancellationToken);
        Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken);
    }
}