using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class UserProgram : DomainEntityId, IDomainAppUserId, IDomainAppUser<AppUser>
    {
        public int FullProgramId { get; set; }

        public FullProgram? FullProgram { get; set; }

        public AppUser? AppUser { get; set; }
        public int AppUserId { get; set; }
    }
}