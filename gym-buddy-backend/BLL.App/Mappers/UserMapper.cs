using AutoMapper;
using BLL.App.DTO.Identity;
using BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class UserMapper : BaseMapper<AppUser, DAL.App.DTO.Identity.AppUser>
    {
        public UserMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}