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
    public class SplitService :
        BaseEntityService<IAppUnitOfWork, ISplitRepository, BLLAppDTO.Split, DALAppDTO.Split>,
        ISplitService
    {
        public SplitService(IAppUnitOfWork serviceUow, ISplitRepository serviceRepository, IMapper mapper) :
            base(serviceUow, serviceRepository, new SplitMapper(mapper))
        {
        }

        public async Task<BLLAppDTO.Split?> FirstOrDefaultWithWorkoutsAsync(int id, int userId = default,
            bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultWithWorkoutsAsync(id, userId, noTracking));
        }
    }
}