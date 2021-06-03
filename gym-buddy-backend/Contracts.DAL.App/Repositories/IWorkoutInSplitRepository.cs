using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IWorkoutInSplitRepository :
        IBaseRepository<WorkoutInSplit>, IWorkoutInSplitRepositoryCustom<WorkoutInSplit>
    {
    }

    public interface IWorkoutInSplitRepositoryCustom<TEntity>
    {
        Task<TEntity?> FirstOrDefaultByWorkoutIdAndSplitId(int workoutId, int splitId, bool noTracking = true);
    }
}