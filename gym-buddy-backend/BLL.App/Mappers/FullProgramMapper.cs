using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.App.DTO;
using BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class FullProgramMapper : BaseMapper<FullProgram, DAL.App.DTO.FullProgram>
    {
        public FullProgramMapper(IMapper mapper) : base(mapper)
        {
        }

        public override FullProgram? Map(DAL.App.DTO.FullProgram? inObject)
        {
            return new()
            {
                Id = inObject!.Id,
                Name = inObject.Name,
                Description = inObject.Description,
                Goal = inObject.Goal,
                SplitsInProgram = inObject.SplitsInProgram?
                    .Select(x => new Split
                    {
                        Id = x.Split.Id,
                        Name = x.Split.Name,
                        Description = x.Split.Description
                    })
                    .ToList() ?? new List<Split>()
            };
        }
    }
}