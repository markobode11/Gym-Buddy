using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.Mappers;
using BLL.App.Services;
using Contracts.DAL.App;
using DAL.App.DTO.MappingProfiles;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain.App;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace TestProject.UnitTests
{
    public class ExerciseServiceAndBaseServiceUnitTests
    {
        private readonly ExerciseService _exerciseService;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly AppDbContext _ctx;

        public ExerciseServiceAndBaseServiceUnitTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;

            var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            // provide new random database name here
            // or parallel test methods will conflict each other
            optionBuilder.UseInMemoryDatabase("test-db-2");
            _ctx = new AppDbContext(optionBuilder.Options);
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();

            var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<AutoMapperProfile>();
                    cfg.AddProfile<BLL.App.DTO.MappingProfiles.AutoMapperProfile>();
                    cfg.AddProfile<PublicAPI.DTO.v1.MappingProfiles.AutoMapperProfile>();
                }
            ).CreateMapper();

            var uow = new AppUnitOfWork(_ctx, mapper);
            var repo = new ExerciseRepository(_ctx, mapper);
            _exerciseService = new ExerciseService(uow, repo, mapper);
        }

        private async Task SeedData(int count = 5)
        {
            for (int i = 0; i < count; i++)
            {
                var exercise = new Exercise()
                {
                    Name = $"Exercise {i + 1}",
                    Description = $"Description for exercise {i + 1}",
                    Difficulty = new Difficulty
                    {
                        Name = $"Difficulty {i + 1}"
                    },
                };

                var muscle = new Muscle
                {
                    MedicalName = $"Medical name {i + 1}",
                    EverydayName = $"Everyday name {i + 1}"
                };

                await _ctx.MuscleInExercises.AddAsync(new MuscleInExercise
                {
                    Muscle = muscle,
                    Exercise = exercise
                });
            }

            await _ctx.SaveChangesAsync();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        public async Task? Test_GetAllWithTrainedMuscles__Returns_Exercises_With_Muscles_And_Difficulty(int count)
        {
            // ARRANGE
            await SeedData(count);

            // ACT
            var result = await _exerciseService.GetAllWithTrainedMuscles();

            // ASSERT
            result.Should().NotBeNull();
            result.Should().NotContainNulls();
            result.Count.Should().Be(count);
            foreach (var each in result)
            {
                each.MusclesTrainedInExercise.Should().NotBeNullOrEmpty();
                each.Difficulty.Should().NotBeNull();
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public async Task? Test_FirstOrDefaultWithMusclesAsync__Returns_Exercise_With_Muscles_And_Difficulty(int count)
        {
            // ARRANGE
            await SeedData(count);

            // ACT
            var first = await _exerciseService.FirstOrDefaultWithMusclesAsync(1);
            var last = await _exerciseService.FirstOrDefaultWithMusclesAsync(count);

            // ASSERT
            first.Should().NotBeNull();
            last.Should().NotBeNull();

            first!.MusclesTrainedInExercise.Should().NotBeNullOrEmpty();
            last!.MusclesTrainedInExercise.Should().NotBeNullOrEmpty();

            first.Difficulty.Should().NotBeNull();
            last.Difficulty.Should().NotBeNull();

            first.Name.Should().Be("Exercise 1");
            last.Name.Should().Be($"Exercise {count}");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public async Task? Test_FirstOrDefaultWithMusclesAsync__Returns_Null_If_Not_Found(int count)
        {
            // ARRANGE
            await SeedData(count);

            // ACT
            var shouldNotExist = await _exerciseService.FirstOrDefaultWithMusclesAsync(count + 1);

            shouldNotExist.Should().BeNull();
        }
    }
}