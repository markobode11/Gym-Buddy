using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class FullProgramRepository :
        BaseRepository<DTO.FullProgram, FullProgram, AppDbContext>, IFullProgramRepository
    {
        public FullProgramRepository(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, new FullProgramMapper(mapper))
        {
        }

        public async Task<DTO.FullProgram?> FirstOrDefaultWithSplitsAsync(int id, int userId = default,
            bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            return Mapper.Map(await query
                .Include(e => e.SplitsInProgram)
                .ThenInclude<FullProgram, SplitInProgram, Split>(x => x.Split)
                .FirstOrDefaultAsync(e => e.Id.Equals(id)));
        }
    }
}