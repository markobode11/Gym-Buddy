using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

#nullable enable
namespace Domain.App
{
    public class UserMentor : DomainEntityId, IDomainAppUserId, IDomainAppUser<AppUser>
    {
        public int MentorId { get; set; }

        public Mentor? Mentor { get; set; }

        public AppUser? AppUser { get; set; }
        public int AppUserId { get; set; }
    }
}