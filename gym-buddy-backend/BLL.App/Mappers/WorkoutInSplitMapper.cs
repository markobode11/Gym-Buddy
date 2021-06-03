using AutoMapper;
using BLL.App.DTO;
using BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class WorkoutInSplitMapper : BaseMapper<WorkoutInSplit, DAL.App.DTO.WorkoutInSplit>
    {
        public WorkoutInSplitMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}