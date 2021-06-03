using AutoMapper;
using BLL.App.DTO;
using BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class SplitInFullProgramMapper : BaseMapper<SplitInProgram, DAL.App.DTO.SplitInProgram>
    {
        public SplitInFullProgramMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}