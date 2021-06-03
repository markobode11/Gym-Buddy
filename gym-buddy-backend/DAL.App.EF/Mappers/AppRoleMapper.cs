using AutoMapper;
using DAL.App.DTO.Identity;
using DAL.Base.EF.Mappers;

namespace DAL.App.EF.Mappers
{
    public class AppRoleMapper : BaseMapper<AppRole, Domain.App.Identity.AppRole>
    {
        public AppRoleMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}