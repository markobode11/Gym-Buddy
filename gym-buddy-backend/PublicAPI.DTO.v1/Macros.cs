using Contracts.Domain.Base;
using Domain.Base;
using PublicAPI.DTO.v1.Enums;

#nullable enable
namespace PublicAPI.DTO.v1
{
    public class Macros : DomainEntityId, IDomainAppUserId
    {
        public int Kcal { get; set; }

        public int Carbs { get; set; }

        public int Protein { get; set; }

        public int Fat { get; set; }

        public char MorGorL { get; set; } // maintain or weight gain or weight loss

        public int AppUserId { get; set; }
    }

    public class MacrosCalculation : DomainEntityId
    {
        public char MorGorL { get; set; }

        public int Height { get; set; }

        public int Weight { get; set; }

        public int Age { get; set; }

        public EGender Gender { get; set; }
    }
}