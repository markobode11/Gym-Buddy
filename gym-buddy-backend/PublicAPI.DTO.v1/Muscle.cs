#nullable enable
using Domain.Base;

namespace PublicAPI.DTO.v1
{
    public class Muscle : DomainEntityId
    {
        public string MedicalName { get; set; } = null!;
        public string EverydayName { get; set; } = null!;
        public string Relevance { get; set; } = null!;
    }
}