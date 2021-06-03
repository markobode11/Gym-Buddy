using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO.Identity;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IUserService :
        IBaseEntityService<AppUser, global::DAL.App.DTO.Identity.AppUser>,
        IUserRepositoryCustom<AppUser>
    {
        Task<IEnumerable<AppUser>> GetAllMentorsTrainees(int userId);
    }
}