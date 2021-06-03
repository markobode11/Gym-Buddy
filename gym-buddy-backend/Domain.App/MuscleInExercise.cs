using Domain.Base;

#nullable enable
namespace Domain.App
{
    public class MuscleInExercise : DomainEntityId
    {
        public string Relevance { get; set; } = null!;

        public int ExerciseId { get; set; }

        public Exercise Exercise { get; set; } = null!;

        public int MuscleId { get; set; }

        public Muscle Muscle { get; set; } = null!;
    }
}