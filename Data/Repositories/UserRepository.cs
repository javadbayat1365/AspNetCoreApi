using Common;
using Common.Exceptions;
using Common.Utilities;
using Data.Contracts;
using Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository, IAsMarkScopeDependency
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<User> GetByUserAndPassAsync(string Username, string password, CancellationToken cancellationToken)
        {
            var passwordHash = SecurityHelper.GetSha256Hash(password);
            return Table.Where(w => w.UserName == Username && w.PasswordHash == passwordHash).SingleOrDefaultAsync(cancellationToken);
        }
        public Task AddAsync(User user, string Password,CancellationToken cancellationToken)
        {
            var sel = TableNoTracking.AnyAsync(p => p.UserName == user.UserName, cancellationToken);
            if (sel != null)
            {
                throw new BadRequestException("این کاربر قبلا ثبت شده است",sel);
            }


            string passwordHash = SecurityHelper.GetSha256Hash(Password);
            user.PasswordHash = passwordHash;
            return base.AddAsync(user,cancellationToken);
        }

        public Task UpdateSecurityStampAsync(User user,CancellationToken cancellationToken)
        {
            user.SecurityStamp = Guid.NewGuid().ToString();
            return UpdateAsync(user, cancellationToken);
        }

        public Task UpdateLastLoginDateAsync(User user,CancellationToken cancellationToken)
        {
            user.LastLoginDate = DateTimeOffset.Now;
            return UpdateAsync(user, cancellationToken);
        }
        public override Task UpdateAsync(User entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            entity.SecurityStamp = Guid.NewGuid().ToString();
            return base.UpdateAsync(entity, cancellationToken, saveNow);
        }
    }
}
