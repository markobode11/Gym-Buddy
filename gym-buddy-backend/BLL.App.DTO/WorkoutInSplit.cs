using Domain.Base;

namespace BLL.App.DTO
{
    public class WorkoutInSplit : DomainEntityId
    {
        public int WorkoutId { get; set; }

        public int SplitId { get; set; }
    }
}