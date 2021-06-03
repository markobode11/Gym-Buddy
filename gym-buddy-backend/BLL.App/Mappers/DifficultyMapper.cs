using AutoMapper;
using BLL.App.DTO;
using BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class DifficultyMapper : BaseMapper<Difficulty, DAL.App.DTO.Difficulty>
    {
        public DifficultyMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}