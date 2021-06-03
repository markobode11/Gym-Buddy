using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class DifficultiesRepository :
        BaseRepository<Difficulty, Domain.App.Difficulty, AppDbContext>,
        IDifficultiesRepository
    {
        public DifficultiesRepository(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, new DifficultyMapper(mapper))
        {
        }
    }
}