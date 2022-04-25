using AutoMapper.QueryableExtensions;
using Data.Contracts;
using Entities.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.ApiFilter;

namespace WebFramework.Api
{
    [ApiVersion("1")]
    public class CrudController<TDto,TSelectDto, TEntity, TKey> : BaseController
        where TDto : BaseDto<TDto, TEntity, TKey>, new()
        where TSelectDto : BaseDto<TSelectDto, TEntity, TKey>, new()
        where TEntity : BaseEntity<TKey>, new()
    {
        public IGenericRepository<TEntity> StoreRepository { get; }

        public CrudController(IGenericRepository<TEntity> storeRepository)
        {
            StoreRepository = storeRepository;
        }

        [HttpGet]
        public virtual async Task<ApiResult<List<TSelectDto>>> Get(CancellationToken cancellationToken)
        {
            var ListDto = await StoreRepository.TableNoTracking.ProjectTo<TSelectDto>()
                .ToListAsync(cancellationToken);
            return Ok(ListDto);
        }

        [HttpGet]
        [Route("{id=long}")]
        public virtual async Task<ApiResult<TSelectDto>> Get(TKey id, CancellationToken cancellationToken)
        {
            var storeDto = await StoreRepository.TableNoTracking.ProjectTo<TSelectDto>()
                .SingleOrDefaultAsync(w => w.Id.Equals(id), cancellationToken);
            if (storeDto == null)
                return BadRequest("لیست فروشگاه خالی است");
            return storeDto;
        }

        [HttpPost]
        public virtual async Task<ApiResult<TSelectDto>> Create(TDto storeDto, CancellationToken cancellationToken)
        {
            var store = storeDto.ToEntity();
            await StoreRepository.AddAsync(store, cancellationToken);
            var result = await StoreRepository.TableNoTracking.ProjectTo<TSelectDto>().SingleOrDefaultAsync(s => s.Id.Equals(store.Id), cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id=long}")]
        public virtual async Task<ApiResult<TSelectDto>> Update(TKey id, TDto storeDto, CancellationToken cancellationToken)
        {
            var store = await StoreRepository.GetByIdAsync(cancellationToken, id);
            store = storeDto.ToEntity(store);
            await StoreRepository.UpdateAsync(store, cancellationToken);
            var StoreDto = StoreRepository.TableNoTracking.ProjectTo<TSelectDto>().SingleOrDefaultAsync(w => w.Id.Equals(store.Id), cancellationToken);

            return Ok(StoreDto);
        }

        [HttpDelete("{id=long}")]
        public virtual async Task<ApiResult> Delete(long id, CancellationToken cancellationToken)
        {
            await StoreRepository.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
    public class CrudController<TDto, TSelectDto, TEntity> : CrudController<TDto, TSelectDto, TEntity, long>
        where TDto : BaseDto<TDto, TEntity, long>, new()
        where TSelectDto : BaseDto<TSelectDto, TEntity, long>, new()
        where TEntity : BaseEntity<long>, new()
    {
        public CrudController(IGenericRepository<TEntity> storeRepository)
            : base(storeRepository)
        {
        }
    }
    public class CrudController<TDto, TEntity> : CrudController<TDto, TDto, TEntity, long>
        where TDto : BaseDto<TDto, TEntity, long>, new()
        where TEntity : BaseEntity<long>, new()
    {
        public CrudController(IGenericRepository<TEntity> storeRepository)
            : base(storeRepository)
        {
        }
    }

}
