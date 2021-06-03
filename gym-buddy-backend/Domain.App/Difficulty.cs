#nullable enable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Difficulty : DomainEntityId
    {
        [MaxLength(64)] public string Name { get; set; } = null!;

        public ICollection<Exercise>? Exercises { get; set; }
    }
}