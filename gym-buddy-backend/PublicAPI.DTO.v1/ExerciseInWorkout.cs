using Domain.Base;

namespace PublicAPI.DTO.v1
{
    public class ExerciseInWorkout : DomainEntityId
    {
        public int ExerciseId { get; set; }

        public int WorkoutId { get; set; }
    }
}