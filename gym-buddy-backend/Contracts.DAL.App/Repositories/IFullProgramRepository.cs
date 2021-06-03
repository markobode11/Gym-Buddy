using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IFullProgramRepository :
        IBaseRepository<FullProgram>,
        IFullProgramRepositoryCustom<FullProgram>
    {
    }

    public interface IFullProgramRepositoryCustom<TEntity>
    {
        Task<TEntity?> FirstOrDefaultWithSplitsAsync(int id, int userId = default, bool noTracking = true);
    }
}