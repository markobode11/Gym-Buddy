using AutoMapper;
using BLL.Base.Mappers;

namespace PublicAPI.DTO.v1.Mappers
{
    public class WorkoutMapper : BaseMapper<Workout, BLL.App.DTO.Workout>
    {
        public WorkoutMapper(IMapper mapper) : base(mapper)
        {
        }

        public BLL.App.DTO.Workout? MapSimple(WorkoutSimple? inObject)
        {
            return Mapper.Map<BLL.App.DTO.Workout>(inObject);
        }

        public WorkoutSimple? MapSimple(BLL.App.DTO.Workout? inObject)
        {
            return Mapper.Map<WorkoutSimple>(inObject);
        }
    }
}