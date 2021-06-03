﻿using System.Threading.Tasks;
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
    public class WorkoutService :
        BaseEntityService<IAppUnitOfWork, IWorkoutRepository, BLLAppDTO.Workout, DALAppDTO.Workout>,
        IWorkoutService
    {
        public WorkoutService(IAppUnitOfWork serviceUow, IWorkoutRepository serviceRepository, IMapper mapper) :
            base(serviceUow, serviceRepository, new WorkoutMapper(mapper))
        {
        }

        public async Task<BLLAppDTO.Workout?> FirstOrDefaultWithExercisesAsync(int id, int userId = default,
            bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultWithExercisesAsync(id, userId, noTracking));
        }
    }
}