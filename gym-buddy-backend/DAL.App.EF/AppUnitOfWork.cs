using AutoMapper;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Repositories;
using DAL.Base.EF;
using DAL.Base.EF.Mappers;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        protected IMapper Mapper;

        public AppUnitOfWork(AppDbContext uowDbContext, IMapper mapper) : base(uowDbContext)
        {
            Mapper = mapper;
        }

        public IExerciseRepository Exercises =>
            GetRepository(() => new ExerciseRepository(UowDbContext, Mapper));

        public IWorkoutRepository Workouts =>
            GetRepository(() => new WorkoutRepository(UowDbContext, Mapper));

        public ISplitRepository Splits =>
            GetRepository(() => new SplitRepository(UowDbContext, Mapper));

        public IFullProgramRepository Programs =>
            GetRepository(() => new FullProgramRepository(UowDbContext, Mapper));

        public IMentorRepository Mentors =>
            GetRepository(() => new MentorRepository(UowDbContext, Mapper));

        public IUserProgramRepository UserPrograms =>
            GetRepository(() => new UserProgramRepository(UowDbContext, Mapper));

        public IUserRepository AppUsers =>
            GetRepository(() => new UserRepository(UowDbContext, Mapper));

        public IDifficultiesRepository Difficulties =>
            GetRepository(() => new DifficultiesRepository(UowDbContext, Mapper));

        public IExerciseInWorkoutRepository ExerciseInWorkoutRepository =>
            GetRepository(() => new ExerciseInWorkoutRepository(UowDbContext, Mapper));

        public IWorkoutInSplitRepository WorkoutInSplitRepository =>
            GetRepository(() => new WorkoutInSplitRepository(UowDbContext, Mapper));

        public ISplitInFullProgramRepository SplitInFullProgramRepository =>
            GetRepository(() => new SplitInFullProgramRepository(UowDbContext, Mapper));

        public IMuscleRepository MuscleRepository =>
            GetRepository(() => new MuscleRepository(UowDbContext, Mapper));

        public IMuscleInExerciseRepository MuscleInExerciseRepository =>
            GetRepository(() => new MuscleInExerciseRepository(UowDbContext, Mapper));

        public IMacrosRepository Macros =>
            GetRepository(() =>
                new MacrosRepository(UowDbContext, new BaseMapper<Macros, Domain.App.Macros>(Mapper)));

        public IUserMentorRepository UserMentorRepository =>
            GetRepository(() =>
                new UserMentorRepository(UowDbContext, new BaseMapper<UserMentor, Domain.App.UserMentor>(Mapper)));
    }
}