using Domain.Base;

namespace Domain.App
{
    public class SplitInProgram : DomainEntityId
    {
        public int SplitId { get; set; }

        public Split Split { get; set; } = null!;

        public int FullProgramId { get; set; }

        public FullProgram FullProgram { get; set; } = null!;
    }
}