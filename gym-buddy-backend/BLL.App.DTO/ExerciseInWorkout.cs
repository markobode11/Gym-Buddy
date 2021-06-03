using Domain.Base;

namespace BLL.App.DTO
{
    public class ExerciseInWorkout : DomainEntityId
    {
        public int ExerciseId { get; set; }

        public int WorkoutId { get; set; }
    }
}