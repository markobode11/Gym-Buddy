#nullable enable
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Difficulty : DomainEntityId
    {
        [MaxLength(64)] public string Name { get; set; } = null!;
    }
}