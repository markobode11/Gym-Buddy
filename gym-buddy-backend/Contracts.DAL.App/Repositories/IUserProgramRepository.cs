using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IUserProgramRepository :
        IBaseRepository<UserProgram>, IUserProgramRepositoryCustom<UserProgram>
    {
    }

    public interface IUserProgramRepositoryCustom<TEntity>
    {
        Task<TEntity> FirstOrDefaultByUserIdAndProgramIdAsync(int programId, int userId);
    }
}