using AutoMapper;
using DAL.App.DTO;
using DAL.Base.EF.Mappers;

namespace DAL.App.EF.Mappers
{
    public class ExerciseInWorkoutMapper : BaseMapper<ExerciseInWorkout, Domain.App.ExerciseInWorkout>
    {
        public ExerciseInWorkoutMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}