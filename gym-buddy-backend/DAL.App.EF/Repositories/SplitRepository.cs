using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class SplitRepository :
        BaseRepository<DTO.Split, Split, AppDbContext>, ISplitRepository
    {
        public SplitRepository(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, new SplitMapper(mapper))
        {
        }

        public async Task<DTO.Split?> FirstOrDefaultWithWorkoutsAsync(int id, int userId = default,
            bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            return Mapper.Map(await query
                .Include(e => e.WorkoutsInSplit)
                .ThenInclude<Split, WorkoutInSplit, Workout>(x => x.Workout)
                .FirstOrDefaultAsync(e => e.Id.Equals(id)));
        }
    }
}