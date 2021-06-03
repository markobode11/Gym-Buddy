using AutoMapper;
using DAL.App.DTO;
using DAL.Base.EF.Mappers;

namespace DAL.App.EF.Mappers
{
    public class UserProgramMapper : BaseMapper<UserProgram, Domain.App.UserProgram>
    {
        public UserProgramMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}