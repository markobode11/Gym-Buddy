#nullable enable
using Domain.Base;

namespace BLL.App.DTO
{
    public class Muscle : DomainEntityId
    {
        public string MedicalName { get; set; } = null!;
        public string EverydayName { get; set; } = null!;
        public string Relevance { get; set; } = null!;
    }
}