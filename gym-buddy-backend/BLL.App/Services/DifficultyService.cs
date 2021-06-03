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
    public class DifficultyService :
        BaseEntityService<IAppUnitOfWork, IDifficultiesRepository, BLLAppDTO.Difficulty, DALAppDTO.Difficulty>,
        IDifficultiesService
    {
        public DifficultyService(IAppUnitOfWork serviceUow, IDifficultiesRepository serviceRepository,
            IMapper mapper) : base(serviceUow, serviceRepository, new DifficultyMapper(mapper))
        {
        }
    }
}