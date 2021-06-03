using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class WorkoutInSplitRepository :
        BaseRepository<WorkoutInSplit, Domain.App.WorkoutInSplit, AppDbContext>,
        IWorkoutInSplitRepository
    {
        public WorkoutInSplitRepository(AppDbContext dbContext,
            IMapper mapper) : base(dbContext, new WorkoutInSplitMapper(mapper))
        {
        }

        public async Task<WorkoutInSplit?> FirstOrDefaultByWorkoutIdAndSplitId(int workoutId, int splitId,
            bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            return Mapper.Map(await query
                .FirstOrDefaultAsync(e => e.SplitId.Equals(splitId) && e.WorkoutId.Equals(workoutId)));
        }
    }
}