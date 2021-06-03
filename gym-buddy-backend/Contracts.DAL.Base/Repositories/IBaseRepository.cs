using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Domain.Base;

namespace Contracts.DAL.Base.Repositories
{
    public interface IBaseRepository<TEntity> : IBaseRepository<TEntity, int>
        where TEntity : class, IDomainEntityId
    {
    }

    public interface IBaseRepository<TEntity, TKey>
        where TEntity : class, IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        // CRUD methods, sync and async
        Task<IEnumerable<TEntity>> GetAllAsync(TKey? userId = default, bool noTracking = true);
        Task<TEntity?> FirstOrDefaultAsync(TKey id, TKey? userId = default, bool noTracking = true);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        Task<TEntity> RemoveAsync(TKey id, TKey? userId = default);
        Task<bool> ExistsAsync(TKey id, TKey? userId = default);
        TEntity GetUpdatedEntityAfterSaveChanges(TEntity entity);
    }
}