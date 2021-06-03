using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        IExerciseRepository Exercises { get; }
        IWorkoutRepository Workouts { get; }
        ISplitRepository Splits { get; }
        IFullProgramRepository Programs { get; }
        IMentorRepository Mentors { get; }
        IUserProgramRepository UserPrograms { get; }
        IUserRepository AppUsers { get; }
        IDifficultiesRepository Difficulties { get; }
        IExerciseInWorkoutRepository ExerciseInWorkoutRepository { get; }
        IWorkoutInSplitRepository WorkoutInSplitRepository { get; }
        ISplitInFullProgramRepository SplitInFullProgramRepository { get; }
        IMuscleRepository MuscleRepository { get; }
        IMuscleInExerciseRepository MuscleInExerciseRepository { get; }
        IMacrosRepository Macros { get; }
        IUserMentorRepository UserMentorRepository { get; }
    }
}