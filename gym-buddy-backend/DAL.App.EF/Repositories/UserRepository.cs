using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.DTO.Identity;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class UserRepository :
        BaseRepository<AppUser, Domain.App.Identity.AppUser, AppDbContext>,
        IUserRepository
    {
        private readonly IMapper _mapper;

        public UserRepository(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, new UserMapper(mapper))
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserProgram>> GetAllUserProgramsByIdAsync(int appUserId,
            bool noTracking = true)
        {
            var mapper = new UserProgramMapper(_mapper);

            return await RepoDbContext.UserPrograms
                .Where(x => x.AppUserId == appUserId)
                .Include(x => x.AppUser)
                .Include(x => x.FullProgram)
                .Select(x => mapper.Map(x)!)
                .ToListAsync();
        }

        public async Task<UserProgram?> FirstOrDefaultUserProgram(int appUserId, int userProgramId,
            bool noTracking = true)
        {
            var mapper = new UserProgramMapper(_mapper);

            var query = RepoDbContext.UserPrograms
                .Include(x => x.AppUser)
                .Include(x => x.FullProgram)
                .Select(x => mapper.Map(x)!)
                .AsQueryable();

            if (noTracking) query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(
                e => e.Id.Equals(userProgramId) && e.AppUserId == appUserId);
        }
    }
}