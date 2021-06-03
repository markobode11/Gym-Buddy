using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ISplitRepository : IBaseRepository<Split>, ISplitRepositoryCustom<Split>
    {
    }

    public interface ISplitRepositoryCustom<TEntity>
    {
        Task<TEntity?> FirstOrDefaultWithWorkoutsAsync(int id, int userId = default, bool noTracking = true);
    }
}