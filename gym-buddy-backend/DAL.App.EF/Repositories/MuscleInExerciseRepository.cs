using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class MuscleInExerciseRepository :
        BaseRepository<MuscleInExercise, Domain.App.MuscleInExercise, AppDbContext>,
        IMuscleInExerciseRepository
    {
        public MuscleInExerciseRepository(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, new MuscleInExerciseMapper(mapper))
        {
        }

        public async Task<MuscleInExercise?> FirstOrDefaultByMuscleIdAndExerciseId(int muscleId, int exerciseId,
            bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            return Mapper.Map(await query
                .FirstOrDefaultAsync(e => e.ExerciseId.Equals(exerciseId) && e.MuscleId.Equals(muscleId)));
        }
    }
}