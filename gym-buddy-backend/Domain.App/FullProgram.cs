#nullable enable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class FullProgram : DomainEntityId
    {
        [MaxLength(64)] [MinLength(3)] public string Name { get; set; } = null!;

        [MaxLength(128)] [MinLength(3)] public string Goal { get; set; } = null!;

        [MaxLength(512)] [MinLength(3)] public string Description { get; set; } = null!;

        public ICollection<SplitInProgram>? SplitsInProgram { get; set; }

        public ICollection<UserProgram>? UserPrograms { get; set; }
    }
}