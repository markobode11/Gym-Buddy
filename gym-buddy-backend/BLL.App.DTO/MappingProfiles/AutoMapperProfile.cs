using AutoMapper;
using BLL.App.DTO.Identity;

namespace BLL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Exercise, DAL.App.DTO.Exercise>().ReverseMap();
            CreateMap<Workout, DAL.App.DTO.Workout>().ReverseMap();
            CreateMap<Split, DAL.App.DTO.Split>().ReverseMap();
            CreateMap<FullProgram, DAL.App.DTO.FullProgram>().ReverseMap();
            CreateMap<Mentor, DAL.App.DTO.Mentor>().ReverseMap();
            CreateMap<AppUser, DAL.App.DTO.Identity.AppUser>().ReverseMap();
            CreateMap<AppRole, DAL.App.DTO.Identity.AppRole>().ReverseMap();

            CreateMap<MuscleInExercise, DAL.App.DTO.MuscleInExercise>().ReverseMap();
            CreateMap<Muscle, DAL.App.DTO.Muscle>().ReverseMap();
            CreateMap<Difficulty, DAL.App.DTO.Difficulty>().ReverseMap();
            CreateMap<ExerciseInWorkout, DAL.App.DTO.ExerciseInWorkout>().ReverseMap();
            CreateMap<WorkoutInSplit, DAL.App.DTO.WorkoutInSplit>().ReverseMap();
            CreateMap<SplitInProgram, DAL.App.DTO.SplitInProgram>().ReverseMap();
            CreateMap<Macros, DAL.App.DTO.Macros>().ReverseMap();
            CreateMap<UserMentor, DAL.App.DTO.UserMentor>().ReverseMap();
            CreateMap<UserProgram, DAL.App.DTO.UserProgram>().ReverseMap();
        }
    }
}