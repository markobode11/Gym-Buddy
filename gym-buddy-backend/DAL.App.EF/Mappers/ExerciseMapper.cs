using AutoMapper;
using DAL.App.DTO;
using DAL.Base.EF.Mappers;

namespace DAL.App.EF.Mappers
{
    public class ExerciseMapper : BaseMapper<Exercise, Domain.App.Exercise>
    {
        public ExerciseMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}