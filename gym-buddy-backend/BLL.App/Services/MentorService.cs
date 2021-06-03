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
    public class MentorService :
        BaseEntityService<IAppUnitOfWork, IMentorRepository, BLLAppDTO.Mentor, DALAppDTO.Mentor>,
        IMentorService
    {
        public MentorService(IAppUnitOfWork serviceUow, IMentorRepository serviceRepository, IMapper mapper) :
            base(serviceUow, serviceRepository, new MentorMapper(mapper))
        {
        }

        public async Task<BLLAppDTO.Mentor> FirstOrDefaultByUserIdAsync(int userId)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultByUserIdAsync(userId))!;
        }
    }
}