#nullable enable
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Muscle : DomainEntityId
    {
        [MaxLength(128)] public string MedicalName { get; set; } = null!;

        [MaxLength(128)] public string EverydayName { get; set; } = null!;
    }
}