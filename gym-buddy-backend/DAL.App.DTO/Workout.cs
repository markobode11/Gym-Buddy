#nullable enable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Workout : DomainEntityId
    {
        [MaxLength(64)] [MinLength(3)] public string Name { get; set; } = null!;

        [MaxLength(512)] [MinLength(3)] public string Description { get; set; } = null!;

        [MaxLength(64)] public string Duration { get; set; } = null!;
        public int DifficultyId { get; set; }

        public Difficulty? Difficulty { get; set; }

        public ICollection<ExerciseInWorkout>? ExercisesInWorkout { get; set; }
    }
}