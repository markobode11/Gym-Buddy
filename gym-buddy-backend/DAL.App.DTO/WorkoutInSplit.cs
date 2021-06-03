using Domain.Base;

namespace DAL.App.DTO
{
    public class WorkoutInSplit : DomainEntityId
    {
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; } = null!;

        public int SplitId { get; set; }
    }
}