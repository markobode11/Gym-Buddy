using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.App.DTO;
using BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class SplitMapper : BaseMapper<Split, DAL.App.DTO.Split>
    {
        public SplitMapper(IMapper mapper) : base(mapper)
        {
        }

        public override Split? Map(DAL.App.DTO.Split? inObject)
        {
            return new()
            {
                Id = inObject!.Id,
                Name = inObject.Name,
                Description = inObject.Description,
                WorkoutsInSplit = inObject.WorkoutsInSplit?
                    .Select(x => new Workout
                    {
                        Id = x.Workout.Id,
                        Name = x.Workout.Name,
                        Description = x.Workout.Description,
                        Duration = x.Workout.Duration,
                        Difficulty = x.Workout.Difficulty != null
                            ? new Difficulty
                            {
                                Id = x.Workout.Difficulty.Id,
                                Name = x.Workout.Difficulty.Name
                            }
                            : null
                    })
                    .ToList() ?? new List<Workout>()
            };
        }
    }
}