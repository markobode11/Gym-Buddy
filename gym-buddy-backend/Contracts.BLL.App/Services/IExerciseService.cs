using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IExerciseService :
        IBaseEntityService<BLLAppDTO.Exercise, DALAppDTO.Exercise>,
        IExerciseRepositoryCustom<BLLAppDTO.Exercise>
    {
        new Task<BLLAppDTO.Exercise?> FirstOrDefaultWithMusclesAsync(int id, int userId = default,
            bool noTracking = true);
    }
}