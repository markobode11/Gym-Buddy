using System.Linq;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        //mvc controllers
        //dotnet aspnet-codegenerator controller -name ExercisesController        -actions -m  Exercise        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        //dotnet aspnet-codegenerator controller -name WorkoutsController        -actions -m  Workout        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        //dotnet aspnet-codegenerator controller -name SplitsController        -actions -m  Split        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        //dotnet aspnet-codegenerator controller -name FullProgramsController        -actions -m  FullProgram        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        //dotnet aspnet-codegenerator controller -name MentorsController        -actions -m  Mentor        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        //dotnet aspnet-codegenerator controller -name UserProgramsController        -actions -m  UserProgram        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f


        //identity admin controllers
        //dotnet aspnet-codegenerator controller -name RolesController        -actions -m  AppRole        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
        //dotnet aspnet-codegenerator controller -name UsersController        -actions -m  AppUser        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

        //not yet created
        //dotnet aspnet-codegenerator controller -name UserMentorsController        -actions -m  UserMentor        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

        //api controllers created
        // dotnet aspnet-codegenerator controller -name ExercisesController     -m Exercise     -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
        // dotnet aspnet-codegenerator controller -name WorkoutsController     -m Workout     -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
        // dotnet aspnet-codegenerator controller -name SplitsController     -m Split     -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
        // dotnet aspnet-codegenerator controller -name FullProgramsController     -m FullProgram     -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
        // dotnet aspnet-codegenerator controller -name MentorsController     -m Mentor     -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
        // dotnet aspnet-codegenerator controller -name UserProgramsController     -m UserProgram     -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
        // dotnet aspnet-codegenerator controller -name MusclesController     -m Domain.App.Muscle     -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
        // dotnet aspnet-codegenerator controller -name MuscleInExerciseController     -m Domain.App.MuscleInExercise     -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
        // dotnet aspnet-codegenerator controller -name DifficultiesController     -m Domain.App.Difficulty     -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
        // dotnet aspnet-codegenerator controller -name ExerciseInWorkoutController     -m Domain.App.ExerciseInWorkout     -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
        // dotnet aspnet-codegenerator controller -name WorkoutInSplitController     -m Domain.App.Split     -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
        // dotnet aspnet-codegenerator controller -name SplitInProgramController     -m Domain.App.SplitInProgram     -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
        // dotnet aspnet-codegenerator controller -name MacrosController     -m Domain.App.Macros     -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
        // dotnet aspnet-codegenerator controller -name UserMentorController     -m Domain.App.UserMentor     -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f


        // dotnet aspnet-codegenerator controller -name UserProgramsController     -m UserProgram     -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f


        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Exercise> Exercises { get; set; } = null!;
        public DbSet<Split> Splits { get; set; } = null!;
        public DbSet<Workout> Workouts { get; set; } = null!;
        public DbSet<FullProgram> FullPrograms { get; set; } = null!;
        public DbSet<Difficulty> Difficulties { get; set; } = null!;
        public DbSet<Muscle> Muscles { get; set; } = null!;
        public DbSet<Mentor> Mentors { get; set; } = null!;
        public DbSet<Macros> Macros { get; set; } = null!;

        public DbSet<UserMentor> UserMentors { get; set; } = null!;
        public DbSet<UserProgram> UserPrograms { get; set; } = null!;
        public DbSet<ExerciseInWorkout> ExerciseInWorkouts { get; set; } = null!;
        public DbSet<WorkoutInSplit> WorkoutInSplits { get; set; } = null!;
        public DbSet<SplitInProgram> SplitInPrograms { get; set; } = null!;
        public DbSet<MuscleInExercise> MuscleInExercises { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //removing the cascade delete, can add back later if needed
            foreach (var relationship in modelBuilder.Model
                .GetEntityTypes()
                .Where(e => !e.IsOwned())
                .SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Entity<AppUser>()
                .Property(b => b.WeightInKg)
                .HasPrecision(4, 2);

            modelBuilder.Entity<Muscle>()
                .HasMany(x => x.MuscleInExercises)
                .WithOne(x => x.Muscle)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Exercise>()
                .HasMany(x => x.MusclesInExercise)
                .WithOne(x => x.Exercise)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Exercise>()
                .HasMany(x => x.ExercisesInWorkout)
                .WithOne(x => x.Exercise)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Workout>()
                .HasMany(x => x.ExercisesInWorkout)
                .WithOne(x => x.Workout)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Workout>()
                .HasMany(x => x.WorkoutsInSplit)
                .WithOne(x => x.Workout)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Split>()
                .HasMany(x => x.WorkoutsInSplit)
                .WithOne(x => x.Split)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Split>()
                .HasMany(x => x.SplitsInPrograms)
                .WithOne(x => x.Split)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FullProgram>()
                .HasMany(x => x.SplitsInProgram)
                .WithOne(x => x.FullProgram)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Mentor>()
                .HasMany(x => x.UserMentors)
                .WithOne(x => x.Mentor!)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FullProgram>()
                .HasMany(x => x.UserPrograms)
                .WithOne(x => x.FullProgram!)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}