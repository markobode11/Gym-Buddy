using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IMuscleInExerciseRepository :
        IBaseRepository<MuscleInExercise>,
        IMuscleInExerciseRepositoryCustom<MuscleInExercise>
    {
    }

    public interface IMuscleInExerciseRepositoryCustom<TEntity>
    {
        Task<TEntity?> FirstOrDefaultByMuscleIdAndExerciseId(int muscleId, int exerciseId,
            bool noTracking = true);
    }
}