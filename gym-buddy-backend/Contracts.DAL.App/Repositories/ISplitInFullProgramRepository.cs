using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ISplitInFullProgramRepository :
        IBaseRepository<SplitInProgram>, ISplitInFullProgramRepositoryCustom<SplitInProgram>
    {
    }

    public interface ISplitInFullProgramRepositoryCustom<TEntity>
    {
        Task<TEntity?> FirstOrDefaultByProgramIdAndSplitId(int programId, int splitId, bool noTracking = true);
    }
}