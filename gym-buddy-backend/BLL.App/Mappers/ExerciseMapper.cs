using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.App.DTO;
using BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class ExerciseMapper : BaseMapper<Exercise, DAL.App.DTO.Exercise>
    {
        public ExerciseMapper(IMapper mapper) : base(mapper)
        {
        }

        public override Exercise? Map(DAL.App.DTO.Exercise? inObject)
        {
            if (inObject == null) return null;

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
                MusclesTrainedInExercise = inObject.MusclesInExercise?
                    .Select(x => new Muscle
                    {
                        Id = x.Muscle.Id,
                        EverydayName = x.Muscle.EverydayName,
                        MedicalName = x.Muscle.MedicalName,
                        Relevance = x.Relevance
                    })
                    .ToList() ?? new List<Muscle>()
            };
        }
    }
}