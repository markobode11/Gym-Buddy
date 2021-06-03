using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.App.DTO;
using BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class WorkoutMapper : BaseMapper<Workout, DAL.App.DTO.Workout>
    {
        public WorkoutMapper(IMapper mapper) : base(mapper)
        {
        }

        public override Workout? Map(DAL.App.DTO.Workout? inObject)
        {
            return new()
            {
                Id = inObject!.Id,
                Name = inObject.Name,
                Description = inObject.Description,
                DifficultyId = inObject.DifficultyId,
                Difficulty = inObject.Difficulty != null
                    ? new Difficulty
                    {
                        Id = inObject.Difficulty.Id,
                        Name = inObject.Difficulty!.Name
                    }
                    : null,
                ExercisesInWorkout = inObject.ExercisesInWorkout?
                    .Select(x => new Exercise
                    {
                        Id = x.Exercise.Id,
                        Name = x.Exercise.Name,
                        Description = x.Exercise.Description,
                        Difficulty = x.Exercise.Difficulty != null
                            ? new Difficulty
                            {
                                Id = x.Exercise.Difficulty.Id,
                                Name = x.Exercise.Difficulty!.Name
                            }
                            : null
                    })
                    .ToList() ?? new List<Exercise>(),
                Duration = inObject.Duration
            };
        }
    }
}