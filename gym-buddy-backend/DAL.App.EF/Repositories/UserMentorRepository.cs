using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class UserMentorRepository :
        BaseRepository<UserMentor, Domain.App.UserMentor, AppDbContext>,
        IUserMentorRepository
    {
        public UserMentorRepository(AppDbContext dbContext, IBaseMapper<UserMentor, Domain.App.UserMentor> mapper) :
            base(dbContext, mapper)
        {
        }

        public async Task<UserMentor?> FirstOrDefaultByUserIdAsync(int getUserId, bool noTracking = true)
        {
            var query = CreateQuery(getUserId, noTracking);

            return Mapper.Map(await query.Where(x => x.AppUserId == getUserId).FirstOrDefaultAsync());
        }

        public async Task<IEnumerable<UserMentor>> GetAllByMentorIdAsync(int mentorId, bool noTracking = true)
        {
            var query = CreateQuery();
            return await query.Where(x => x.MentorId == mentorId).Select(x => Mapper.Map(x)!).ToListAsync();
        }
    }
}