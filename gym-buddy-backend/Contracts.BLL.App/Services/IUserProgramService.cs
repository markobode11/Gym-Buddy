using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IUserProgramService :
        IBaseEntityService<BLLAppDTO.UserProgram, DALAppDTO.UserProgram>,
        IUserProgramRepositoryCustom<BLLAppDTO.UserProgram>
    {
        new Task<BLLAppDTO.UserProgram> FirstOrDefaultByUserIdAndProgramIdAsync(int programId, int userId);
    }
}