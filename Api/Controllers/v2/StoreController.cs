using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Models;
using AutoMapper.QueryableExtensions;
using Data.Contracts;
using Entities.Store;
using Marketer.Utilities.GenericMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFramework.Api;

namespace Api.Controllers.v2
{
    [ApiVersion("2")]
    public class StoreController : v1.StoreController
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
        #endregion

        [HttpGet]
        [Route("{id=long}")]
        public override async Task<ApiResult<StoreSelectDto>> Get(long id, CancellationToken cancellationToken)
        {
            var result = await StoreRepository.TableNoTracking.ProjectTo<StoreSelectDto>().SingleOrDefaultAsync(w => w.Id == id, cancellationToken);
            result.ShamsiRegisterDate = result.ShamsiRegisterDate == "" ? DateTime.Now.ToShamsiDateYMD() : result.ShamsiRegisterDate;
            return result;
        }

        public override Task<ApiResult<StoreSelectDto>> Update(long id, StoreDto storeDto, CancellationToken cancellationToken)
        {
            return base.Update(id, storeDto, cancellationToken);
        }
    }
}