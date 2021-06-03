using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IUserMentorService :
        IBaseEntityService<BLLAppDTO.UserMentor, DALAppDTO.UserMentor>,
        IUserMentorRepositoryCustom<BLLAppDTO.UserMentor>
    {
    }
}