using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IFullProgramService :
        IBaseEntityService<BLLAppDTO.FullProgram, DALAppDTO.FullProgram>,
        IFullProgramRepositoryCustom<BLLAppDTO.FullProgram>
    {
        Task<IEnumerable<BLLAppDTO.FullProgram>> GetAllUserFullPrograms(int userId, bool noTracking = true);
    }
}