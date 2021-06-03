using Domain.Base;

namespace Domain.App
{
    public class WorkoutInSplit : DomainEntityId
    {
        public int WorkoutId { get; set; }

        public Workout Workout { get; set; } = null!;

        public int SplitId { get; set; }

        public Split Split { get; set; } = null!;
    }
}