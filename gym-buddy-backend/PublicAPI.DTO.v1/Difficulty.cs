#nullable enable
using Domain.Base;

namespace PublicAPI.DTO.v1
{
    public class Difficulty : DomainEntityId
    {
        public string Name { get; set; } = null!;
    }
}