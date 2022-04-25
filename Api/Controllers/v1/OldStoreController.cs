using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Contracts;
using Entities.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFramework.Api;
using WebFramework.ApiFilter;

namespace Api.Controllers.v1
{
    [ApiVersion("1")]
    public class OldStoreController : BaseController
    {
        public IStoreRepository StoreRepository { get; }

        public OldStoreController(IStoreRepository storeRepository)
        {
            StoreRepository = storeRepository;
        }

        [HttpGet]
        public async Task<ApiResult<List<StoreDto>>> Get(CancellationToken cancellationToken)
        {

            #region Old Code
            //var ListDto = await StoreRepository.TableNoTracking.Include(i => i.user).Select(s => new StoreDto
            //{
            //    Id = s.Id,
            //    Lat = s.Lat,
            //    Lng = s.Lng,
            //    Mobile = s.Mobile,
            //    Phone = s.Phone,
            //    SellerName = s.SellerName,
            //    Plaque = s.Plaque,
            //    RegisterDate = s.RegisterDate,
            //    StoreAddress = s.StoreAddress,
            //    StoreName = s.StoreName,
            //    UserName = s.user.Fullname,
            //    vahed = s.vahed
            //}).ToListAsync();


            //var ListDto = await StoreRepository.TableNoTracking.Include(i => i.user).ToListAsync();
            //var ListDto = result.Select(s => {
            //    var dto = Mapper.Map<StoreDto>(s);
            //    return dto;
            //}).ToList();
            #endregion

            //OR
            var ListDto = await StoreRepository.TableNoTracking.ProjectTo<StoreDto>()
                //.Where(w => w.SellerName.Contains("aria"))
                .ToListAsync(cancellationToken);
            return Ok(ListDto);
        }

        [HttpGet]
        [Route("{id=long}")]
        public async Task<ApiResult<StoreDto>> Get(long id, CancellationToken cancellationToken)
        {
            var storeDto = await StoreRepository.TableNoTracking.ProjectTo<StoreDto>()
                .SingleOrDefaultAsync(w => w.Id == id, cancellationToken);
            if (storeDto == null)
                return BadRequest("لیست فروشگاه خالی است");
            return storeDto;
        }

        [HttpPost]
        public async Task<ApiResult<StoreDto>> Create(StoreDto storeDto, CancellationToken cancellationToken)
        {
            var store = storeDto.ToEntity();// Mapper.Map<Store>(storeDto);
            await StoreRepository.AddAsync(store, cancellationToken);
            var result = await StoreRepository.TableNoTracking.ProjectTo<StoreDto>().SingleOrDefaultAsync(s => s.Id == store.Id, cancellationToken);
            
            //var StoreDto = result.ToEntity(); //Mapper.Map<StoreDto>(result);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ApiResult<StoreDto>> Update(long id, StoreDto storeDto, CancellationToken cancellationToken)
        {
            var store = await StoreRepository.GetByIdAsync(cancellationToken, id);
            store = storeDto.ToEntity(store);//Mapper.Map(storeDto, store);
            await StoreRepository.UpdateAsync(store, cancellationToken);
            var StoreDto = StoreRepository.TableNoTracking.ProjectTo<StoreDto>().SingleOrDefaultAsync(w => w.Id == store.Id,cancellationToken);
            
            return Ok(StoreDto);
        }

        [HttpDelete("{id=long}")]
        public async Task<ApiResult> Delete(long id, CancellationToken cancellationToken)
        {
            await StoreRepository.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}