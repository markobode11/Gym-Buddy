using AutoMapper;
using BLL.Base.Mappers;

namespace PublicAPI.DTO.v1.Mappers
{
    public class FullProgramMapper : BaseMapper<FullProgram, BLL.App.DTO.FullProgram>
    {
        public FullProgramMapper(IMapper mapper) : base(mapper)
        {
        }

        public BLL.App.DTO.FullProgram? MapSimple(FullProgramSimple? inObject)
        {
            return Mapper.Map<BLL.App.DTO.FullProgram>(inObject);
        }

        public FullProgramSimple? MapSimple(BLL.App.DTO.FullProgram? inObject)
        {
            return Mapper.Map<FullProgramSimple>(inObject);
        }
    }
}