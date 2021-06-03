using Domain.Base;

namespace PublicAPI.DTO.v1
{
    public class SplitInProgram : DomainEntityId
    {
        public int SplitId { get; set; }

        public int FullProgramId { get; set; }
    }
}