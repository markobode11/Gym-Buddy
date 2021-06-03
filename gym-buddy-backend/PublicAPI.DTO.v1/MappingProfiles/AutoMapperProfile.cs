using AutoMapper;
using PublicAPI.DTO.v1.Identity;

namespace PublicAPI.DTO.v1.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Exercise, BLL.App.DTO.Exercise>().ReverseMap();
            CreateMap<Workout, BLL.App.DTO.Workout>().ReverseMap();
            CreateMap<Split, BLL.App.DTO.Split>().ReverseMap();
            CreateMap<FullProgram, BLL.App.DTO.FullProgram>().ReverseMap();
            CreateMap<Mentor, BLL.App.DTO.Mentor>().ReverseMap();
            CreateMap<Muscle, BLL.App.DTO.Muscle>().ReverseMap();
            CreateMap<Difficulty, BLL.App.DTO.Difficulty>().ReverseMap();
            CreateMap<MuscleInExercise, BLL.App.DTO.MuscleInExercise>().ReverseMap();
            CreateMap<ExerciseInWorkout, BLL.App.DTO.ExerciseInWorkout>().ReverseMap();
            CreateMap<WorkoutInSplit, BLL.App.DTO.WorkoutInSplit>().ReverseMap();
            CreateMap<SplitInProgram, BLL.App.DTO.SplitInProgram>().ReverseMap();
            CreateMap<Macros, BLL.App.DTO.Macros>().ReverseMap();
            CreateMap<UserMentor, BLL.App.DTO.UserMentor>().ReverseMap();
            CreateMap<UserProgram, BLL.App.DTO.UserProgram>().ReverseMap();

            CreateMap<AppUser, BLL.App.DTO.Identity.AppUser>().ReverseMap();
            CreateMap<Domain.App.Identity.AppUser, AppUserInfo>();
            CreateMap<Domain.App.Identity.AppUser, AppUserInfoWithRoleAndLockDown>();
            CreateMap<Domain.App.Identity.AppUser, AppUserWithRole>();
            CreateMap<AppRole, BLL.App.DTO.Identity.AppRole>().ReverseMap();
            CreateMap<Domain.App.Identity.AppRole, AppRole>();

            CreateMap<WorkoutSimple, BLL.App.DTO.Workout>().ReverseMap();
            CreateMap<ExerciseSimple, BLL.App.DTO.Exercise>().ReverseMap();
            CreateMap<SplitSimple, BLL.App.DTO.Split>().ReverseMap();
            CreateMap<FullProgramSimple, BLL.App.DTO.FullProgram>().ReverseMap();
            CreateMap<MentorSimple, BLL.App.DTO.Mentor>().ReverseMap();
            CreateMap<MacrosCalculation, BLL.App.DTO.MacrosCalculation>().ReverseMap();
        }
    }
}