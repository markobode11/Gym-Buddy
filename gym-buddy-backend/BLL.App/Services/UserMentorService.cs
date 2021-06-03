using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Mappers;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class UserMentorService :
        BaseEntityService<IAppUnitOfWork, IUserMentorRepository, BLLAppDTO.UserMentor,
            DALAppDTO.UserMentor>,
        IUserMentorService
    {
        public UserMentorService(IAppUnitOfWork serviceUow, IUserMentorRepository serviceRepository,
            IBaseMapper<BLLAppDTO.UserMentor, DALAppDTO.UserMentor> mapper) : base(serviceUow, serviceRepository,
            mapper)
        {
        }

        public async Task<BLLAppDTO.UserMentor?> FirstOrDefaultByUserIdAsync(int getUserId, bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultByUserIdAsync(getUserId, noTracking));
        }

        public async Task<IEnumerable<BLLAppDTO.UserMentor>> GetAllByMentorIdAsync(int mentorId, bool noTracking = true)
        {
            return (await ServiceRepository.GetAllByMentorIdAsync(mentorId, noTracking))
                .Select(x => Mapper.Map(x)!);
        }
    }
}