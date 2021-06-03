using AutoMapper;
using DAL.App.DTO;
using DAL.Base.EF.Mappers;

namespace DAL.App.EF.Mappers
{
    public class FullProgramMapper : BaseMapper<FullProgram, Domain.App.FullProgram>
    {
        public FullProgramMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}