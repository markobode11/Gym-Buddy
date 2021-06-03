using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IExerciseRepository : IBaseRepository<Exercise>, IExerciseRepositoryCustom<Exercise>
    {
    }

    public interface IExerciseRepositoryCustom<TEntity>
    {
        // custom methods here
        Task<List<TEntity>> GetAllWithTrainedMuscles(int userId = default, bool noTracking = true);
        Task<TEntity?> FirstOrDefaultWithMusclesAsync(int id, int userId = default, bool noTracking = true);
    }
}