using AutoMapper;
using BLL.Base.Mappers;

namespace PublicAPI.DTO.v1.Mappers
{
    public class SplitMapper : BaseMapper<Split, BLL.App.DTO.Split>
    {
        public SplitMapper(IMapper mapper) : base(mapper)
        {
        }

        public BLL.App.DTO.Split? MapSimple(SplitSimple? inObject)
        {
            return Mapper.Map<BLL.App.DTO.Split>(inObject);
        }

        public SplitSimple? MapSimple(BLL.App.DTO.Split? inObject)
        {
            return Mapper.Map<SplitSimple>(inObject);
        }
    }
}