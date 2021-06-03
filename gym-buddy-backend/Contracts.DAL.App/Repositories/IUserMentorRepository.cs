using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IUserMentorRepository :
        IBaseRepository<UserMentor>, IUserMentorRepositoryCustom<UserMentor>
    {
    }

    public interface IUserMentorRepositoryCustom<TEntity>
    {
        Task<TEntity?> FirstOrDefaultByUserIdAsync(int getUserId, bool noTracking = true);
        Task<IEnumerable<TEntity>> GetAllByMentorIdAsync(int mentorId, bool noTracking = true);
    }
}