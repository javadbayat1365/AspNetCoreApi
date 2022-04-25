
using Api.Models;
using Data.Contracts;
using Entities.Store;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Api;

namespace Api.Controllers.v1
{
    public class StoreController : CrudController<StoreDto, StoreSelectDto, Store, long>
    {
        public StoreController(IGenericRepository<Store> storeRepository)
            : base(storeRepository)
        {
        }
    }
}