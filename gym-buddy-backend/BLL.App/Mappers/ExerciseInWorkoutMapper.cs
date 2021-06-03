using AutoMapper;
using BLL.App.DTO;
using BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class ExerciseInWorkoutMapper : BaseMapper<ExerciseInWorkout, DAL.App.DTO.ExerciseInWorkout>
    {
        public ExerciseInWorkoutMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}