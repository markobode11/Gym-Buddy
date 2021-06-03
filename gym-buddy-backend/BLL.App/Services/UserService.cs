using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.DTO.Identity;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class UserService :
        BaseEntityService<IAppUnitOfWork, IUserRepository, AppUser, DAL.App.DTO.Identity.AppUser>,
        IUserService
    {
        private IMapper _mapper;

        public UserService(
            IAppUnitOfWork serviceUow,
            IUserRepository serviceRepository,
            IMapper mapper) :
            base(serviceUow, serviceRepository, new UserMapper(mapper))
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppUser>> GetAllMentorsTrainees(int userId)
        {
            var connectedMentor = await ServiceUow.Mentors.FirstOrDefaultByUserIdAsync(userId);
            var mentorUsers = await ServiceUow.UserMentorRepository.GetAllByMentorIdAsync(connectedMentor.Id);
            var trainees = new List<AppUser>();
            foreach (var user in mentorUsers)
                trainees.Add(Mapper.Map(await ServiceUow.AppUsers.FirstOrDefaultAsync(user.AppUserId))!);

            return trainees;
        }
    }
}