using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IMacrosService :
        IBaseEntityService<BLLAppDTO.Macros, DALAppDTO.Macros>,
        IMacrosRepositoryCustom<BLLAppDTO.Macros>
    {
        BLLAppDTO.Macros CalculateMacros(BLLAppDTO.MacrosCalculation bllEntity);
    }
}