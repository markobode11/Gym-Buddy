using Contracts.Domain.Base;
using Domain.Base;

namespace PublicAPI.DTO.v1
{
    public class UserProgram : DomainEntityId, IDomainAppUserId
    {
        public int FullProgramId { get; set; }
        public int AppUserId { get; set; }
    }
}