using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IMuscleInExerciseService :
        IBaseEntityService<BLLAppDTO.MuscleInExercise, DALAppDTO.MuscleInExercise>,
        IMuscleInExerciseRepositoryCustom<BLLAppDTO.MuscleInExercise>
    {
        new Task<BLLAppDTO.MuscleInExercise?> FirstOrDefaultByMuscleIdAndExerciseId(int muscleId, int exerciseId,
            bool noTracking = true);
    }
}