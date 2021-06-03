using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IDifficultiesRepository :
        IBaseRepository<Difficulty>, IDifficultiesRepositoryCustom<Difficulty>
    {
    }

    public interface IDifficultiesRepositoryCustom<TEntity>
    {
    }
}