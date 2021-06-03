using AutoMapper;
using BLL.Base.Mappers;

namespace PublicAPI.DTO.v1.Mappers
{
    public class ExerciseMapper : BaseMapper<Exercise, BLL.App.DTO.Exercise>
    {
        public ExerciseMapper(IMapper mapper) : base(mapper)
        {
        }

        public ExerciseSimple? MapSimple(BLL.App.DTO.Exercise? exercise)
        {
            return Mapper.Map<ExerciseSimple>(exercise);
        }

        public BLL.App.DTO.Exercise? MapSimple(ExerciseSimple? exercise)
        {
            return Mapper.Map<BLL.App.DTO.Exercise>(exercise);
        }
    }
}