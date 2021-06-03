using Domain.Base;

namespace Domain.App
{
    public class ExerciseInWorkout : DomainEntityId
    {
        public int ExerciseId { get; set; }

        public Exercise Exercise { get; set; } = null!;

        public int WorkoutId { get; set; }

        public Workout Workout { get; set; } = null!;
    }
}