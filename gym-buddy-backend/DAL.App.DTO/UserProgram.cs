using Contracts.Domain.Base;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class UserProgram : DomainEntityId, IDomainAppUserId, IDomainAppUser<AppUser>
    {
        public int FullProgramId { get; set; }

        public FullProgram? FullProgram { get; set; }

        public AppUser? AppUser { get; set; }
        public int AppUserId { get; set; }
    }
}