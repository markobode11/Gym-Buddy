using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class MacrosRepository :
        BaseRepository<Macros, Domain.App.Macros, AppDbContext>,
        IMacrosRepository
    {
        public MacrosRepository(AppDbContext dbContext, IBaseMapper<Macros, Domain.App.Macros> mapper) : base(dbContext,
            mapper)
        {
        }

        public async Task<Macros?> FirstOrDefaultByUserIdAsync(int userId, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            return Mapper.Map(await query
                .FirstOrDefaultAsync(e => e.AppUserId.Equals(userId)));
        }

        public async Task<bool> ExistsAsync(int userId)
        {
            return await RepoDbSet.AnyAsync(e =>
                e.AppUserId.Equals(userId));
        }
    }
}