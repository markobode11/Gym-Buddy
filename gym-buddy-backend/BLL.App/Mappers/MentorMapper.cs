using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.App.DTO;
using BLL.App.DTO.Identity;
using BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class MentorMapper : BaseMapper<Mentor, DAL.App.DTO.Mentor>
    {
        public MentorMapper(IMapper mapper) : base(mapper)
        {
        }

        public override Mentor? Map(DAL.App.DTO.Mentor? inObject)
        {
            if (inObject == null) return null;
            return new()
            {
                Id = inObject.Id,
                Description = inObject.Description,
                FullName = inObject.FullName,
                Since = inObject.Since,
                Email = inObject.Email,
                Specialty = inObject.Specialty,
                AppUserId = inObject.AppUserId,
                MentorUsers = inObject.UserMentors?
                    .Select(x => Mapper.Map<AppUser>(x))
                    .ToList() ?? new List<AppUser>()
            };
        }
    }
}