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
    public class ExerciseInWorkoutService :
        BaseEntityService<IAppUnitOfWork, IExerciseInWorkoutRepository, BLLAppDTO.ExerciseInWorkout,
            DALAppDTO.ExerciseInWorkout>,
        IExerciseInWorkoutService
    {
        public ExerciseInWorkoutService(IAppUnitOfWork serviceUow, IExerciseInWorkoutRepository serviceRepository,
            IMapper mapper) : base(serviceUow,
            serviceRepository, new ExerciseInWorkoutMapper(mapper))
        {
        }

        public async Task<BLLAppDTO.ExerciseInWorkout?> FirstOrDefaultByWorkoutIdAndExerciseId(int workoutId,
            int exerciseId, bool noTracking = true)
        {
            return Mapper.Map(
                await ServiceRepository.FirstOrDefaultByWorkoutIdAndExerciseId(workoutId, exerciseId, noTracking));
        }
    }
}