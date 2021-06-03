using Domain.Base;

namespace DAL.App.DTO
{
    public class SplitInProgram : DomainEntityId
    {
        public int SplitId { get; set; }
        public Split Split { get; set; } = null!;

        public int FullProgramId { get; set; }
    }
}