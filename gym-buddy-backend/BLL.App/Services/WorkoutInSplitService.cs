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
    public class WorkoutInSplitService :
        BaseEntityService<IAppUnitOfWork, IWorkoutInSplitRepository, BLLAppDTO.WorkoutInSplit,
            DALAppDTO.WorkoutInSplit>,
        IWorkoutInSplitService
    {
        public WorkoutInSplitService(IAppUnitOfWork serviceUow, IWorkoutInSplitRepository serviceRepository,
            IMapper mapper) : base(serviceUow,
            serviceRepository, new WorkoutInSplitMapper(mapper))
        {
        }

        public async Task<BLLAppDTO.WorkoutInSplit?> FirstOrDefaultByWorkoutIdAndSplitId(int workoutId, int splitId,
            bool noTracking = true)
        {
            return Mapper.Map(
                await ServiceRepository.FirstOrDefaultByWorkoutIdAndSplitId(workoutId, splitId, noTracking));
        }
    }
}