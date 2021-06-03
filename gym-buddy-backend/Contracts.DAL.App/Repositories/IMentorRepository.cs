using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IMentorRepository :
        IBaseRepository<Mentor>, IMentorRepositoryCustom<Mentor>
    {
    }

    public interface IMentorRepositoryCustom<TEntity>
    {
        Task<TEntity> FirstOrDefaultByUserIdAsync(int userId);
    }
}