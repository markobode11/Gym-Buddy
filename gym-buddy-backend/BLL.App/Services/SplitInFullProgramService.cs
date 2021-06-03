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
    public class SplitInFullProgramService :
        BaseEntityService<IAppUnitOfWork, ISplitInFullProgramRepository, BLLAppDTO.SplitInProgram,
            DALAppDTO.SplitInProgram>,
        ISplitInFullProgramService
    {
        public SplitInFullProgramService(IAppUnitOfWork serviceUow, ISplitInFullProgramRepository serviceRepository,
            IMapper mapper) : base(serviceUow,
            serviceRepository, new SplitInFullProgramMapper(mapper))
        {
        }

        public async Task<BLLAppDTO.SplitInProgram?> FirstOrDefaultByProgramIdAndSplitId(int programId, int splitId,
            bool noTracking = true)
        {
            return Mapper.Map(
                await ServiceRepository.FirstOrDefaultByProgramIdAndSplitId(programId, splitId, noTracking));
        }
    }
}