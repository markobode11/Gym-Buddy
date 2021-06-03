using AutoMapper;
using BLL.Base.Mappers;

namespace PublicAPI.DTO.v1.Mappers
{
    public class MuscleMapper : BaseMapper<Muscle, BLL.App.DTO.Muscle>
    {
        public MuscleMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}