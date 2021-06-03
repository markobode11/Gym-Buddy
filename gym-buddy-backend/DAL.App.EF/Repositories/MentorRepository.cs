using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class MentorRepository :
        BaseRepository<Mentor, Domain.App.Mentor, AppDbContext>, IMentorRepository
    {
        public MentorRepository(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, new MentorMapper(mapper))
        {
        }

        public async Task<Mentor> FirstOrDefaultByUserIdAsync(int userId)
        {
            return Mapper.Map(await RepoDbSet.FirstOrDefaultAsync(x => x.AppUserId == userId))!;
        }
    }
}