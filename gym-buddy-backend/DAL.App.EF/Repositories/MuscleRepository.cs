using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class MuscleRepository :
        BaseRepository<Muscle, Domain.App.Muscle, AppDbContext>, IMuscleRepository
    {
        public MuscleRepository(AppDbContext dbContext, IMapper mapper)
            : base(dbContext, new MuscleMapper(mapper))
        {
        }
    }
}