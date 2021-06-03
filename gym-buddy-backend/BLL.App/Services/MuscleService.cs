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
    public class MuscleService :
        BaseEntityService<IAppUnitOfWork, IMuscleRepository, BLLAppDTO.Muscle, DALAppDTO.Muscle>,
        IMuscleService
    {
        public MuscleService(IAppUnitOfWork serviceUow, IMuscleRepository serviceRepository,
            IMapper mapper) : base(serviceUow, serviceRepository, new MuscleMapper(mapper))
        {
        }
    }
}