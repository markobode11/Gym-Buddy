using AutoMapper;
using BLL.App.DTO;
using BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class MuscleInExerciseMapper : BaseMapper<MuscleInExercise, DAL.App.DTO.MuscleInExercise>
    {
        public MuscleInExerciseMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}