using AutoMapper;
using BLL.App.DTO.Identity;
using BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class AppRoleMapper : BaseMapper<AppRole, DAL.App.DTO.Identity.AppRole>
    {
        public AppRoleMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}