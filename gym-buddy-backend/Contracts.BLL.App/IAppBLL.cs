using Contracts.BLL.App.Services;
using Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        IExerciseService Exercises { get; }
        IWorkoutService Workouts { get; }
        ISplitService Splits { get; }
        IFullProgramService Programs { get; }
        IMentorService Mentors { get; }
        IUserProgramService UserPrograms { get; }
        IUserService Users { get; }

        IMuscleService Muscles { get; }
        IMuscleInExerciseService MuscleInExercises { get; }
        IDifficultiesService Difficulties { get; }
        IExerciseInWorkoutService ExerciseInWorkouts { get; }
        IWorkoutInSplitService WorkoutInSplits { get; }
        ISplitInFullProgramService SplitInPrograms { get; }
        IMacrosService Macros { get; }
        IUserMentorService UserMentors { get; }
        IAccountService AccountService { get; }
    }
}