using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IMacrosRepository :
        IBaseRepository<Macros>,
        IMacrosRepositoryCustom<Macros>
    {
    }

    public interface IMacrosRepositoryCustom<TEntity>
    {
        Task<TEntity?> FirstOrDefaultByUserIdAsync(int userId, bool noTracking = true);
        Task<bool> ExistsAsync(int userId);
    }
}