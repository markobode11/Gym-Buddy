using Domain.Base;

#nullable enable
namespace PublicAPI.DTO.v1
{
    public class MuscleInExercise : DomainEntityId
    {
        public string Relevance { get; set; } = null!;

        public int ExerciseId { get; set; }

        public int MuscleId { get; set; }
    }
}