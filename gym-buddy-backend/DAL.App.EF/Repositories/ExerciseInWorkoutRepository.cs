using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ExerciseInWorkoutRepository :
        BaseRepository<ExerciseInWorkout, Domain.App.ExerciseInWorkout, AppDbContext>,
        IExerciseInWorkoutRepository
    {
        public ExerciseInWorkoutRepository(AppDbContext dbContext,
            IMapper mapper) : base(dbContext, new ExerciseInWorkoutMapper(mapper))
        {
        }

        public async Task<ExerciseInWorkout?> FirstOrDefaultByWorkoutIdAndExerciseId(int workoutId, int exerciseId,
            bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            return Mapper.Map(await query
                .FirstOrDefaultAsync(e => e.ExerciseId.Equals(exerciseId) && e.WorkoutId.Equals(workoutId)));
        }
    }
}