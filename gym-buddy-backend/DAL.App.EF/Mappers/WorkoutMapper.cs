using AutoMapper;
using DAL.App.DTO;
using DAL.Base.EF.Mappers;

namespace DAL.App.EF.Mappers
{
    public class WorkoutMapper : BaseMapper<Workout, Domain.App.Workout>
    {
        public WorkoutMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}