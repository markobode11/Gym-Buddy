using System.Collections.Generic;
using System.Linq;
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
    public class ExerciseService :
        BaseEntityService<IAppUnitOfWork, IExerciseRepository, BLLAppDTO.Exercise, DALAppDTO.Exercise>,
        IExerciseService
    {
        private IMapper _mapper;

        public ExerciseService(IAppUnitOfWork serviceUow, IExerciseRepository serviceRepository, IMapper mapper)
            : base(serviceUow, serviceRepository, new ExerciseMapper(mapper))
        {
            _mapper = mapper;
        }

        public async Task<List<BLLAppDTO.Exercise>> GetAllWithTrainedMuscles(int userId = default,
            bool noTracking = true)
        {
            return (await ServiceRepository.GetAllWithTrainedMuscles(userId, noTracking))
                .Select(x => Mapper.Map(x)!)
                .ToList();
        }

        public async Task<BLLAppDTO.Exercise?> FirstOrDefaultWithMusclesAsync(int id, int userId = default,
            bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultWithMusclesAsync(id));
        }
    }
}