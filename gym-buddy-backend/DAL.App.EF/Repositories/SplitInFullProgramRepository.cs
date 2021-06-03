using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class SplitInFullProgramRepository :
        BaseRepository<SplitInProgram, Domain.App.SplitInProgram, AppDbContext>,
        ISplitInFullProgramRepository
    {
        public SplitInFullProgramRepository(AppDbContext dbContext,
            IMapper mapper) : base(dbContext, new SplitInFullProgramMapper(mapper))
        {
        }

        public async Task<SplitInProgram?> FirstOrDefaultByProgramIdAndSplitId(int programId, int splitId,
            bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            return Mapper.Map(await query
                .FirstOrDefaultAsync(e => e.SplitId.Equals(splitId) && e.FullProgramId.Equals(programId)));
        }
    }
}