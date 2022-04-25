using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Models;
using Data.Contracts;
using Entities.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Api;

namespace Api.Controllers.v3
{
    [ApiVersion("3")] 
    public class StoreController : v2.StoreController
    {
        public StoreController(IGenericRepository<Store> storeRepository) : base(storeRepository)
        {
        }

        #region This Override Is For Confilict Swagger Not DotNetCore
            public override Task<ApiResult<StoreSelectDto>> Create(StoreDto storeDto, CancellationToken cancellationToken)
            {
                return base.Create(storeDto, cancellationToken);
            }

            public override Task<ApiResult> Delete(long id, CancellationToken cancellationToken)
            {
                return base.Delete(id, cancellationToken);
            }

            public override Task<ApiResult<List<StoreSelectDto>>> Get(CancellationToken cancellationToken)
            {
                return base.Get(cancellationToken);
            }

            public override Task<ApiResult<StoreSelectDto>> Get(long id, CancellationToken cancellationToken)
            {
                return base.Get(id, cancellationToken);
            }
            public override Task<ApiResult<StoreSelectDto>> Update(long id, StoreDto storeDto, CancellationToken cancellationToken)
            {
                return base.Update(id, storeDto, cancellationToken);
            }

        #endregion


        //url => api/v3/store/testv3
        [HttpGet("testv3")]
        public async Task<ApiResult<string>> test()
        {
            return Ok("تست ورژن3");
        }

       
    }
}