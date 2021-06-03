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
    public class FullProgramService :
        BaseEntityService<IAppUnitOfWork, IFullProgramRepository, BLLAppDTO.FullProgram, DALAppDTO.FullProgram>,
        IFullProgramService
    {
        public FullProgramService(IAppUnitOfWork serviceUow, IFullProgramRepository serviceRepository, IMapper mapper) :
            base(serviceUow, serviceRepository, new FullProgramMapper(mapper))
        {
        }

        public async Task<BLLAppDTO.FullProgram?> FirstOrDefaultWithSplitsAsync(int id, int userId = default,
            bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultWithSplitsAsync(id, userId, noTracking));
        }

        public async Task<IEnumerable<BLLAppDTO.FullProgram>> GetAllUserFullPrograms(int userId, bool noTracking = true)
        {
            var userProgramDtoList = await ServiceUow.UserPrograms.GetAllAsync(userId);
            var fullProgramList = await ServiceUow.Programs.GetAllAsync();
            return fullProgramList
                .Where(program => userProgramDtoList.Any(x => x.FullProgramId == program.Id))
                .Select(x => Mapper.Map(x)!);
        }
    }
}