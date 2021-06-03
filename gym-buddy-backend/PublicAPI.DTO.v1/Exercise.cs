#nullable enable
using System.Collections.Generic;
using Domain.Base;

namespace PublicAPI.DTO.v1
{
    public class Exercise : DomainEntityId
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int DifficultyId { get; set; }

        public Difficulty? Difficulty { get; set; }

        public ICollection<Muscle>? MusclesTrainedInExercise { get; set; }
    }

    public class ExerciseSimple : DomainEntityId
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int DifficultyId { get; set; }

        public Difficulty? Difficulty { get; set; }
    }
}