using AutoMapper;
using BLL.App.DTO;
using BLL.App.Services;
using BLL.Base;
using BLL.Base.Mappers;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        protected IMapper Mapper;

        public AppBLL(IAppUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager) : base(uow)
        {
            Mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IExerciseService Exercises =>
            GetService<IExerciseService>(() => new ExerciseService(Uow, Uow.Exercises, Mapper));

        public IWorkoutService Workouts =>
            GetService<IWorkoutService>(() => new WorkoutService(Uow, Uow.Workouts, Mapper));

        public ISplitService Splits =>
            GetService<ISplitService>(() => new SplitService(Uow, Uow.Splits, Mapper));

        public IFullProgramService Programs =>
            GetService<IFullProgramService>(() => new FullProgramService(Uow, Uow.Programs, Mapper));

        public IMentorService Mentors =>
            GetService<IMentorService>(() => new MentorService(Uow, Uow.Mentors, Mapper));

        public IUserProgramService UserPrograms =>
            GetService<IUserProgramService>(() => new UserProgramService(Uow, Uow.UserPrograms, Mapper));

        public IMuscleService Muscles =>
            GetService<IMuscleService>(() => new MuscleService(Uow, Uow.MuscleRepository, Mapper));

        public IMuscleInExerciseService MuscleInExercises =>
            GetService<IMuscleInExerciseService>(() =>
                new MuscleInExerciseService(Uow, Uow.MuscleInExerciseRepository, Mapper));

        public IDifficultiesService Difficulties =>
            GetService<IDifficultiesService>(() => new DifficultyService(Uow, Uow.Difficulties, Mapper));

        public IExerciseInWorkoutService ExerciseInWorkouts =>
            GetService<IExerciseInWorkoutService>(() =>
                new ExerciseInWorkoutService(Uow, Uow.ExerciseInWorkoutRepository, Mapper));

        public IWorkoutInSplitService WorkoutInSplits =>
            GetService<IWorkoutInSplitService>(() =>
                new WorkoutInSplitService(Uow, Uow.WorkoutInSplitRepository, Mapper));

        public ISplitInFullProgramService SplitInPrograms =>
            GetService<ISplitInFullProgramService>(() =>
                new SplitInFullProgramService(Uow, Uow.SplitInFullProgramRepository, Mapper));

        public IUserService Users =>
            GetService<IUserService>(() =>
                new UserService(Uow, Uow.AppUsers, Mapper));

        public IMacrosService Macros =>
            GetService<IMacrosService>(() =>
                new MacrosService(Uow, Uow.Macros, new BaseMapper<Macros, DAL.App.DTO.Macros>(Mapper)));

        public IUserMentorService UserMentors =>
            GetService<IUserMentorService>(() =>
                new UserMentorService(Uow, Uow.UserMentorRepository,
                    new BaseMapper<UserMentor, DAL.App.DTO.UserMentor>(Mapper)));

        public IAccountService AccountService => GetService(() => new AccountService(_userManager, _roleManager));
    }
}