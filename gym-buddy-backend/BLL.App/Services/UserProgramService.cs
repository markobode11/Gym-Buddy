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
    public class UserProgramService :
        BaseEntityService<IAppUnitOfWork, IUserProgramRepository, BLLAppDTO.UserProgram, DALAppDTO.UserProgram>,
        IUserProgramService
    {
        public UserProgramService(IAppUnitOfWork serviceUow, IUserProgramRepository serviceRepository, IMapper mapper) :
            base(serviceUow, serviceRepository, new UserProgramMapper(mapper))
        {
        }

        public async Task<BLLAppDTO.UserProgram> FirstOrDefaultByUserIdAndProgramIdAsync(int programId, int userId)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultByUserIdAndProgramIdAsync(programId, userId))!;
        }
    }
}