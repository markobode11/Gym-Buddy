using AutoMapper;
using DAL.App.DTO;
using DAL.Base.EF.Mappers;

namespace DAL.App.EF.Mappers
{
    public class SplitMapper : BaseMapper<Split, Domain.App.Split>
    {
        public SplitMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}