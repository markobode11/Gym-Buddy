using AutoMapper;
using DAL.App.DTO;
using DAL.Base.EF.Mappers;

namespace DAL.App.EF.Mappers
{
    public class WorkoutInSplitMapper : BaseMapper<WorkoutInSplit, Domain.App.WorkoutInSplit>
    {
        public WorkoutInSplitMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}