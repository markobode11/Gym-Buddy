using Contracts.Domain.Base;
using DAL.App.DTO.Identity;
using Domain.Base;

#nullable enable
namespace DAL.App.DTO
{
    public class UserMentor : DomainEntityId, IDomainAppUserId, IDomainAppUser<AppUser>
    {
        public int MentorId { get; set; }

        public AppUser? AppUser { get; set; }
        public int AppUserId { get; set; }
    }
}