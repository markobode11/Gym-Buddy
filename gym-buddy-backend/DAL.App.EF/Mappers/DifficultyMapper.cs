using AutoMapper;
using DAL.App.DTO;
using DAL.Base.EF.Mappers;

namespace DAL.App.EF.Mappers
{
    public class DifficultyMapper : BaseMapper<Difficulty, Domain.App.Difficulty>
    {
        public DifficultyMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}