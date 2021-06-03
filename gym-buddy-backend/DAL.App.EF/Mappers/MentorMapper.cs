using AutoMapper;
using DAL.App.DTO;
using DAL.Base.EF.Mappers;

namespace DAL.App.EF.Mappers
{
    public class MentorMapper : BaseMapper<Mentor, Domain.App.Mentor>
    {
        public MentorMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}