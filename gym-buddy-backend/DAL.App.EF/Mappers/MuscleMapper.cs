using AutoMapper;
using DAL.App.DTO;
using DAL.Base.EF.Mappers;

namespace DAL.App.EF.Mappers
{
    public class MuscleMapper : BaseMapper<Muscle, Domain.App.Muscle>
    {
        public MuscleMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}