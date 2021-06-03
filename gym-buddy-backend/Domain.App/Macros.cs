using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

#nullable enable
namespace Domain.App
{
    public class Macros : DomainEntityId, IDomainAppUserId, IDomainAppUser<AppUser>
    {
        public int Kcal { get; set; }

        public int Carbs { get; set; }

        public int Protein { get; set; }

        public int Fat { get; set; }

        public char MorGorL { get; set; } // maintain or weight gain or weight loss

        public AppUser? AppUser { get; set; }
        public int AppUserId { get; set; }
    }
}