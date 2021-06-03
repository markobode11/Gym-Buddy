using Domain.Base;

namespace BLL.App.DTO
{
    public class SplitInProgram : DomainEntityId
    {
        public int SplitId { get; set; }

        public int FullProgramId { get; set; }
    }
}