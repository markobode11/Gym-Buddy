using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IExerciseInWorkoutRepository :
        IBaseRepository<ExerciseInWorkout>, IExerciseInWorkoutRepositoryCustom<ExerciseInWorkout>
    {
    }

    public interface IExerciseInWorkoutRepositoryCustom<TEntity>
    {
        Task<TEntity?> FirstOrDefaultByWorkoutIdAndExerciseId(int workoutId, int exerciseId, bool noTracking = true);
    }
}