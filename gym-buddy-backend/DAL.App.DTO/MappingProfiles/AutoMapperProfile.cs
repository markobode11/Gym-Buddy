using AutoMapper;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Exercise, Domain.App.Exercise>().ReverseMap();
            CreateMap<Workout, Domain.App.Workout>().ReverseMap();
            CreateMap<Split, Domain.App.Split>().ReverseMap();
            CreateMap<FullProgram, Domain.App.FullProgram>().ReverseMap();
            CreateMap<Mentor, Domain.App.Mentor>().ReverseMap();
            CreateMap<Muscle, Domain.App.Muscle>().ReverseMap();
            CreateMap<Difficulty, Domain.App.Difficulty>().ReverseMap();
            CreateMap<MuscleInExercise, Domain.App.MuscleInExercise>().ReverseMap();
            CreateMap<Muscle, Domain.App.Muscle>().ReverseMap();
            CreateMap<AppUser, Domain.App.Identity.AppUser>().ReverseMap();
            CreateMap<AppRole, Domain.App.Identity.AppRole>().ReverseMap();
            CreateMap<WorkoutInSplit, Domain.App.WorkoutInSplit>().ReverseMap();
            CreateMap<ExerciseInWorkout, Domain.App.ExerciseInWorkout>().ReverseMap();
            CreateMap<SplitInProgram, Domain.App.SplitInProgram>().ReverseMap();
            CreateMap<Macros, Domain.App.Macros>().ReverseMap();
            CreateMap<UserMentor, Domain.App.UserMentor>().ReverseMap();
            CreateMap<UserProgram, Domain.App.UserProgram>().ReverseMap();
        }
    }
}