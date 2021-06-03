using AutoMapper;
using BLL.App.DTO;
using BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class UserProgramMapper : BaseMapper<UserProgram, DAL.App.DTO.UserProgram>
    {
        public UserProgramMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}