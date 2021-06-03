#nullable enable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace PublicAPI.DTO.v1
{
    public class Split : DomainEntityId
    {
        [MaxLength(64)] [MinLength(3)] public string Name { get; set; } = null!;

        [MaxLength(512)] [MinLength(3)] public string Description { get; set; } = null!;

        public ICollection<WorkoutSimple>? WorkoutsInSplit { get; set; }
    }

    public class SplitSimple : DomainEntityId
    {
        [MaxLength(64)] [MinLength(3)] public string Name { get; set; } = null!;

        [MaxLength(512)] [MinLength(3)] public string Description { get; set; } = null!;
    }
}