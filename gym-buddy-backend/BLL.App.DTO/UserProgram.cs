using Contracts.Domain.Base;
using Domain.Base;

namespace BLL.App.DTO
{
    public class UserProgram : DomainEntityId, IDomainAppUserId
    {
        public int FullProgramId { get; set; }
        public int AppUserId { get; set; }
    }
}