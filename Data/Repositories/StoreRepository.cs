using Common;
using Data.Contracts;
using Entities.Store;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class StoreRepository : GenericRepository<Store>, IStoreRepository, IAsMarkScopeDependency
    {
        public StoreRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
