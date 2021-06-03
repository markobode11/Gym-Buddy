using AutoMapper;
using DAL.App.DTO;
using DAL.Base.EF.Mappers;

namespace DAL.App.EF.Mappers
{
    public class SplitInFullProgramMapper : BaseMapper<SplitInProgram, Domain.App.SplitInProgram>
    {
        public SplitInFullProgramMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}