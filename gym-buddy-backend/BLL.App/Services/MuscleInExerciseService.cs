using System.Threading.Tasks;
using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class MuscleInExerciseService :
        BaseEntityService<IAppUnitOfWork, IMuscleInExerciseRepository, BLLAppDTO.MuscleInExercise,
            DALAppDTO.MuscleInExercise>,
        IMuscleInExerciseService
    {
        public MuscleInExerciseService(IAppUnitOfWork serviceUow, IMuscleInExerciseRepository serviceRepository,
            IMapper mapper)
            : base(serviceUow, serviceRepository, new MuscleInExerciseMapper(mapper))
        {
        }

        public async Task<BLLAppDTO.MuscleInExercise?> FirstOrDefaultByMuscleIdAndExerciseId(int muscleId,
            int exerciseId, bool noTracking = true)
        {
            return Mapper.Map(
                await ServiceRepository.FirstOrDefaultByMuscleIdAndExerciseId(muscleId, exerciseId, noTracking));
        }
    }
}