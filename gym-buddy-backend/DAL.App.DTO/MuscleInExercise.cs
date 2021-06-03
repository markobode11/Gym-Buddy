using Domain.Base;

#nullable enable
namespace DAL.App.DTO
{
    public class MuscleInExercise : DomainEntityId
    {
        public string Relevance { get; set; } = null!;

        public int ExerciseId { get; set; }

        public int MuscleId { get; set; }
        public Muscle Muscle { get; set; } = null!;
    }
}