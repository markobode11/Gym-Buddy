using Contracts.Domain.Base;
using Domain.Base;

#nullable enable
namespace PublicAPI.DTO.v1
{
    public class UserMentor : DomainEntityId, IDomainAppUserId
    {
        public int MentorId { get; set; }
        public int AppUserId { get; set; }
    }
}