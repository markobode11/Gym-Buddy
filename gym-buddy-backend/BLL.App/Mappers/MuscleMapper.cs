using AutoMapper;
using BLL.App.DTO;
using BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class MuscleMapper : BaseMapper<Muscle, DAL.App.DTO.Muscle>
    {
        public MuscleMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}