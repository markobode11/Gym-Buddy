#nullable enable
using Domain.Base;

namespace BLL.App.DTO
{
    public class Difficulty : DomainEntityId
    {
        public string Name { get; set; } = null!;
    }
}