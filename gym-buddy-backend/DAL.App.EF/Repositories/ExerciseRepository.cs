using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ExerciseRepository :
        BaseRepository<DTO.Exercise, Exercise, AppDbContext>,
        IExerciseRepository
    {
        public ExerciseRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new ExerciseMapper(mapper))
        {
        }

        public override async Task<IEnumerable<DTO.Exercise>> GetAllAsync(int userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            return await query
                .Include(e => e.Difficulty)
                .Select(x => Mapper.Map(x)!)
                .ToListAsync();
        }

        public override async Task<DTO.Exercise?> FirstOrDefaultAsync(int id, int userId = default,
            bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            return Mapper.Map(await query
                .Include(e => e.Difficulty)
                .FirstOrDefaultAsync(e => e.Id.Equals(id)));
        }

        public async Task<DTO.Exercise?> FirstOrDefaultWithMusclesAsync(int id, int userId = default,
            bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            return Mapper.Map(await query
                .Include(e => e.Difficulty)
                .Include(e => e.MusclesInExercise)
                .ThenInclude(x => x.Muscle)
                .FirstOrDefaultAsync(e => e.Id.Equals(id)));
        }

        public async Task<List<DTO.Exercise>> GetAllWithTrainedMuscles(int userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            return await query
                .Include(e => e.Difficulty)
                .Include(e => e.MusclesInExercise)
                .ThenInclude<Exercise, MuscleInExercise, Muscle>(x => x.Muscle)
                .Select(exercise => Mapper.Map(exercise)!)
                .ToListAsync();
        }
    }
}