using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.App.EF.AppDataInit
{
    public static class DataInit
    {
        public static void DropDatabase(AppDbContext ctx, ILogger logger)
        {
            logger.LogInformation("DropDatabase");
            ctx.Database.EnsureDeleted();
        }

        public static void MigrateDatabase(AppDbContext ctx, ILogger logger)
        {
            logger.LogInformation("MigrateDatabase");
            ctx.Database.Migrate();
        }

        public static async void SeedAppData(AppDbContext ctx, UserManager<AppUser> userManager, ILogger logger)
        {
            var difficulties = await SeedDifficulties(ctx, logger);
            var muscles = await SeedMuscles(ctx, logger);

            var exercises = await SeedExercises(ctx, logger);
            var workouts = await SeedWorkouts(ctx, logger);
            var splits = await SeedSplits(ctx, logger);
            var fullPrograms = await SeedFullPrograms(ctx, logger);
            
            var mentors = await SeedMentors(ctx, logger);

            await SeedMusclesInExercise(muscles, exercises, ctx);

            await SeedExercisesInWorkouts(exercises, workouts, ctx);
            
            await SeedWorkoutsInSplits(workouts, splits, ctx);
            
            await SeedSplitsInPrograms(splits, fullPrograms, ctx);

            await ctx.SaveChangesAsync();
        }

        private static async Task SeedSplitsInPrograms(List<Split> splits, List<FullProgram> fullPrograms, AppDbContext ctx)
        {
            foreach (var fullProgram in fullPrograms)
            {
                if (fullProgram.Name == "Powerbuilding program")
                {
                    ctx.SplitInPrograms.Add(new SplitInProgram()
                    {
                        FullProgramId = fullProgram.Id,
                        SplitId = 1
                    });
                }
                
                if (fullProgram.Name == "Healthy program")
                {
                    ctx.SplitInPrograms.Add(new SplitInProgram()
                    {
                        FullProgramId = fullProgram.Id,
                        SplitId = 1
                    });
                    ctx.SplitInPrograms.Add(new SplitInProgram()
                    {
                        FullProgramId = fullProgram.Id,
                        SplitId = 2
                    });
                    ctx.SplitInPrograms.Add(new SplitInProgram()
                    {
                        FullProgramId = fullProgram.Id,
                        SplitId = 4
                    });
                }
            }
            await ctx.SaveChangesAsync();
        }

        private static async Task SeedWorkoutsInSplits(List<Workout> workouts, List<Split> splits, AppDbContext ctx)
        {
            foreach (var split in splits)
            {

                if (split.Name == "PPL")
                {
                    ctx.WorkoutInSplits.Add(new WorkoutInSplit()
                    {
                        SplitId = split.Id,
                        WorkoutId = 1
                    });
                    ctx.WorkoutInSplits.Add(new WorkoutInSplit()
                    {
                        SplitId = split.Id,
                        WorkoutId = 2
                    });
                    ctx.WorkoutInSplits.Add(new WorkoutInSplit()
                    {
                        SplitId = split.Id,
                        WorkoutId = 3
                    });
                }
                if (split.Name == "Upper lower split")
                {
                    ctx.WorkoutInSplits.Add(new WorkoutInSplit()
                    {
                        SplitId = split.Id,
                        WorkoutId = 4
                    });
                    ctx.WorkoutInSplits.Add(new WorkoutInSplit()
                    {
                        SplitId = split.Id,
                        WorkoutId = 5
                    });
                }
            }
            await ctx.SaveChangesAsync();
        }

        private static async Task SeedExercisesInWorkouts(List<Exercise> exercises, List<Workout> workouts, AppDbContext ctx)
        {
            foreach (var workout in workouts)
            {
                if (workout.Name == "Push")
                {
                    ctx.ExerciseInWorkouts.Add(new ExerciseInWorkout
                    {
                        ExerciseId = 2,
                        WorkoutId = workout.Id
                    });
                    ctx.ExerciseInWorkouts.Add(new ExerciseInWorkout
                    {
                        ExerciseId = 4,
                        WorkoutId = workout.Id
                    });
                    ctx.ExerciseInWorkouts.Add(new ExerciseInWorkout
                    {
                        ExerciseId = 8,
                        WorkoutId = workout.Id
                    });
                }
                
                if (workout.Name == "Pull")
                {
                    ctx.ExerciseInWorkouts.Add(new ExerciseInWorkout
                    {
                        ExerciseId = 3,
                        WorkoutId = workout.Id
                    });
                    ctx.ExerciseInWorkouts.Add(new ExerciseInWorkout
                    {
                        ExerciseId = 5,
                        WorkoutId = workout.Id
                    });
                    ctx.ExerciseInWorkouts.Add(new ExerciseInWorkout
                    {
                        ExerciseId = 6,
                        WorkoutId = workout.Id
                    });
                    ctx.ExerciseInWorkouts.Add(new ExerciseInWorkout
                    {
                        ExerciseId = 7,
                        WorkoutId = workout.Id
                    });
                }
                
                if (workout.Name == "Legs")
                {
                    ctx.ExerciseInWorkouts.Add(new ExerciseInWorkout
                    {
                        ExerciseId = 1,
                        WorkoutId = workout.Id
                    });
                    ctx.ExerciseInWorkouts.Add(new ExerciseInWorkout
                    {
                        ExerciseId = 9,
                        WorkoutId = workout.Id
                    });
                }
            }
            await ctx.SaveChangesAsync();
        }

        private static async Task SeedMusclesInExercise(List<Muscle> muscles, List<Exercise> exercises, AppDbContext ctx)
        {
            foreach (var exercise in exercises)
            {
                if (exercise.Name == "Squat")
                {
                    ctx.MuscleInExercises.Add(new MuscleInExercise
                    {
                        MuscleId = 6,
                        ExerciseId = exercise.Id,
                        Relevance = "Main muscle"
                    });
                    ctx.MuscleInExercises.Add(new MuscleInExercise
                    {
                        MuscleId = 7,
                        ExerciseId = exercise.Id,
                        Relevance = "Secondary muscle"
                    });
                }
                else if (exercise.Name == "Bench press")
                {
                    ctx.MuscleInExercises.Add(new MuscleInExercise
                    {
                        MuscleId = 1,
                        ExerciseId = exercise.Id,
                        Relevance = "Main muscle"
                    });
                    ctx.MuscleInExercises.Add(new MuscleInExercise
                    {
                        MuscleId = 2,
                        ExerciseId = exercise.Id,
                        Relevance = "Secondary muscle"
                    });
                    ctx.MuscleInExercises.Add(new MuscleInExercise
                    {
                        MuscleId = 3,
                        ExerciseId = exercise.Id,
                        Relevance = "Secondary muscle"
                    });
                }
                else if (exercise.Name == "Deadlift")
                {
                    ctx.MuscleInExercises.Add(new MuscleInExercise
                    {
                        MuscleId = 4,
                        ExerciseId = exercise.Id,
                        Relevance = "Main muscle"
                    });
                    ctx.MuscleInExercises.Add(new MuscleInExercise
                    {
                        MuscleId = 5,
                        ExerciseId = exercise.Id,
                        Relevance = "Secondary muscle"
                    });
                    ctx.MuscleInExercises.Add(new MuscleInExercise
                    {
                        MuscleId = 6,
                        ExerciseId = exercise.Id,
                        Relevance = "Secondary muscle"
                    });
                    ctx.MuscleInExercises.Add(new MuscleInExercise
                    {
                        MuscleId = 7,
                        ExerciseId = exercise.Id,
                        Relevance = "Secondary muscle"
                    });
                }
            }

            await ctx.SaveChangesAsync();
        }

        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,
            ILogger logger)
        {
            logger.LogInformation("SeedIdentity");
            foreach (var roleName in InitialData.Roles)
            {
                var role = roleManager.FindByNameAsync(roleName).Result;
                if (role == null)
                {
                    role = new AppRole
                    {
                        Name = roleName,
                    };

                    var result = roleManager.CreateAsync(role).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException($"Role creation failed: {roleName}");
                    }

                    logger.LogInformation("Seeded role {Role}", roleName);
                }
            }

            foreach (var userInfo in InitialData.Users)
            {
                var user = new AppUser
                {
                    Email = userInfo.name,
                    UserName = userInfo.name,
                    Firstname = userInfo.firstName,
                    Lastname = userInfo.lastName,
                    EmailConfirmed = true
                };

                var result = userManager.CreateAsync(user, userInfo.password).Result;
                if (!result.Succeeded)
                {
                    throw new ApplicationException($"User creation failed: {user.Email}");
                }

                logger.LogInformation("Seeded user {User}", userInfo.name);


                if (userInfo.role == null) continue;
                var roleResult = userManager.AddToRoleAsync(user, userInfo.role).Result;
                if (!roleResult.Succeeded)
                {
                    throw new ApplicationException($"Adding roles failed: {user.Email}, {userInfo.role}");
                }
            }
        }

        private static async Task<List<Exercise>> SeedExercises(AppDbContext context, ILogger logger)
        {
            var exercises = new List<Exercise>();
            logger.LogInformation("Seed Exercises");
            foreach (var each in InitialData.Exercises)
            {
                var exercise = new Exercise
                {
                    Name = each.name,
                    Description = each.description,
                    DifficultyId = each.difficultyId
                };
                await context.Exercises.AddAsync(exercise);
                exercises.Add(exercise);
            }

            await context.SaveChangesAsync();
            return exercises;
        }

        private static async Task<List<Workout>> SeedWorkouts(AppDbContext context, ILogger logger)
        {
            var workouts = new List<Workout>();
            logger.LogInformation("Seed workouts");
            foreach (var each in InitialData.Workouts)
            {
                var workout = new Workout()
                {
                    Name = each.name,
                    Description = each.description,
                    Duration = each.duration,
                    DifficultyId = each.difficultyId
                };
                await context.Workouts.AddAsync(workout);
                workouts.Add(workout);
            }

            await context.SaveChangesAsync();
            return workouts;
        }

        private static async Task<List<Split>> SeedSplits(AppDbContext context, ILogger logger)
        {
            var splits = new List<Split>();
            logger.LogInformation("Seed splits");
            foreach (var each in InitialData.Splits)
            {
                var split = new Split()
                {
                    Name = each.name,
                    Description = each.description,
                };
                await context.Splits.AddAsync(split);
                splits.Add(split);
            }

            await context.SaveChangesAsync();
            return splits;
        }

        private static async Task<List<FullProgram>> SeedFullPrograms(AppDbContext context, ILogger logger)
        {
            var programs = new List<FullProgram>();
            logger.LogInformation("Seed programs");
            foreach (var each in InitialData.FullPrograms)
            {
                var program = new FullProgram()
                {
                    Name = each.name,
                    Description = each.description,
                    Goal = each.goal
                };
                await context.FullPrograms.AddAsync(program);
                programs.Add(program);
            }

            await context.SaveChangesAsync();
            return programs;
        }

        private static async Task<List<Muscle>> SeedMuscles(AppDbContext context, ILogger logger)
        {
            var muscles = new List<Muscle>();
            logger.LogInformation("Seed muscles");
            foreach (var each in InitialData.Muscles)
            {
                var muscle = new Muscle()
                {
                    MedicalName = each.medicalName,
                    EverydayName = each.everydayName
                };
                await context.Muscles.AddAsync(muscle);
                muscles.Add(muscle);
            }

            await context.SaveChangesAsync();
            return muscles;
        }

        private static async Task<List<Difficulty>> SeedDifficulties(AppDbContext context, ILogger logger)
        {
            var diffs = new List<Difficulty>();
            logger.LogInformation("Seed difficulties");
            foreach (var each in InitialData.Difficulties)
            {
                var diff = new Difficulty()
                {
                    Name = each
                };
                await context.Difficulties.AddAsync(diff);
                diffs.Add(diff);
            }

            await context.SaveChangesAsync();
            return diffs;
        }

        private static async Task<List<Mentor>> SeedMentors(AppDbContext context, ILogger logger)
        {
            var mentors = new List<Mentor>();
            logger.LogInformation("Seed mentors");
            foreach (var each in InitialData.Mentors)
            {
                var mentor = new Mentor()
                {
                    FullName = each.fullName,
                    AppUserId = each.appUserId,
                    Description = each.description,
                    Specialty = each.specialty,
                    Email = each.email
                };
                await context.Mentors.AddAsync(mentor);
                mentors.Add(mentor);
            }

            await context.SaveChangesAsync();
            return mentors;
        }
    }
}