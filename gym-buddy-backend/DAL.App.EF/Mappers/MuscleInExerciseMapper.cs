using AutoMapper;
using DAL.App.DTO;
using DAL.Base.EF.Mappers;

namespace DAL.App.EF.Mappers
{
    public class MuscleInExerciseMapper : BaseMapper<MuscleInExercise, Domain.App.MuscleInExercise>
    {
        public MuscleInExerciseMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}