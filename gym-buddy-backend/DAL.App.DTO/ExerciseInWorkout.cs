using Domain.Base;

namespace DAL.App.DTO
{
    public class ExerciseInWorkout : DomainEntityId
    {
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; } = null!;

        public int WorkoutId { get; set; }
    }
}