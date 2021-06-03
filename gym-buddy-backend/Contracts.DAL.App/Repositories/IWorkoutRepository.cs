using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IWorkoutRepository : IBaseRepository<Workout>, IWorkoutRepositoryCustom<Workout>
    {
    }

    public interface IWorkoutRepositoryCustom<TEntity>
    {
        Task<TEntity?> FirstOrDefaultWithExercisesAsync(int id, int userId = default, bool noTracking = true);
    }
}