using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class UserProgramRepository :
        BaseRepository<UserProgram, Domain.App.UserProgram, AppDbContext>, IUserProgramRepository
    {
        public UserProgramRepository(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, new UserProgramMapper(mapper))
        {
        }

        public async Task<UserProgram> FirstOrDefaultByUserIdAndProgramIdAsync(int programId, int userId)
        {
            return Mapper.Map(await RepoDbContext
                .UserPrograms
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.AppUserId == userId && x.FullProgramId == programId))!;
        }
    }
}