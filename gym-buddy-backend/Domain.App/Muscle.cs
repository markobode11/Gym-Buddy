#nullable enable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Muscle : DomainEntityId
    {
        [MaxLength(128)] public string MedicalName { get; set; } = null!;

        [MaxLength(128)] public string EverydayName { get; set; } = null!;

        public ICollection<MuscleInExercise>? MuscleInExercises { get; set; }
    }
}