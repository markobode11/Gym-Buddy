using System.Threading.Tasks;
using BLL.App.DTO.Enums;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Mappers;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class MacrosService :
        BaseEntityService<IAppUnitOfWork, IMacrosRepository, BLLAppDTO.Macros, DALAppDTO.Macros>,
        IMacrosService
    {
        public MacrosService(IAppUnitOfWork serviceUow, IMacrosRepository serviceRepository,
            IBaseMapper<BLLAppDTO.Macros, DALAppDTO.Macros> mapper) : base(serviceUow, serviceRepository, mapper)
        {
        }

        public async Task<BLLAppDTO.Macros?> FirstOrDefaultByUserIdAsync(int userId, bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultByUserIdAsync(userId, noTracking));
        }

        public async Task<bool> ExistsAsync(int userId)
        {
            return await ServiceRepository.ExistsAsync(userId);
        }

        public BLLAppDTO.Macros CalculateMacros(BLLAppDTO.MacrosCalculation bllEntity)
        {
            var kcal = bllEntity.Weight * 10 + bllEntity.Height * 6.25 - bllEntity.Age * 5;
            if (bllEntity.Gender == EGender.Male)
                kcal += 5;
            else
                kcal -= 161;

            kcal *= 1.4; // activity factor
            switch (bllEntity.MorGorL)
            {
                case 'G':
                    kcal += 500;
                    break;
                case 'L':
                    kcal -= 500;
                    break;
            }

            return new BLLAppDTO.Macros
            {
                Kcal = (int) kcal,
                Carbs = (int) ((kcal - bllEntity.Weight * 2 * 4 - bllEntity.Weight * 0.7 * 8) / 4),
                Protein = bllEntity.Weight * 2,
                Fat = (int) (bllEntity.Weight * 0.7),
                MorGorL = bllEntity.MorGorL
            };
        }
    }
}