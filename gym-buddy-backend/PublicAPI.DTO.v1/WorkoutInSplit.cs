using Domain.Base;

namespace PublicAPI.DTO.v1
{
    public class WorkoutInSplit : DomainEntityId
    {
        public int WorkoutId { get; set; }

        public int SplitId { get; set; }
    }
}