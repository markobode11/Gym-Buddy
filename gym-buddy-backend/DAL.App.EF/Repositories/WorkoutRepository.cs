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
    public class WorkoutRepository :
        BaseRepository<DTO.Workout, Workout, AppDbContext>, IWorkoutRepository
    {
        public WorkoutRepository(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, new WorkoutMapper(mapper))
        {
        }

        public override async Task<IEnumerable<DTO.Workout>> GetAllAsync(int userId = default,
            bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            return await RepoDbSet
                .Include(e => e.Difficulty)
                .Select(x => Mapper.Map(x)!)
                .ToListAsync();
        }

        public override async Task<DTO.Workout?> FirstOrDefaultAsync(int id, int userId = default,
            bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            return Mapper.Map(await query
                .Include(e => e.Difficulty)
                .FirstOrDefaultAsync(e => e.Id.Equals(id)));
        }

        public async Task<DTO.Workout?> FirstOrDefaultWithExercisesAsync(int id, int userId = default,
            bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            return Mapper.Map(await query
                .Include(e => e.Difficulty)
                .Include(e => e.ExercisesInWorkout)
                .ThenInclude<Workout, ExerciseInWorkout, Exercise>(x => x.Exercise)
                .FirstOrDefaultAsync(e => e.Id.Equals(id)));
        }
    }
}