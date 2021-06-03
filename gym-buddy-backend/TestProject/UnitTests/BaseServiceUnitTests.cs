using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.DTO;
using BLL.App.Services;
using BLL.Base.Services;
using DAL.App.DTO.MappingProfiles;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using DAL.Base.EF.Mappers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace TestProject.UnitTests
{
    public class BaseServiceUnitTests
    {
        private readonly
            BaseEntityService<AppUnitOfWork, ExerciseRepository, BLL.App.DTO.Exercise, DAL.App.DTO.Exercise>
            _baseService;

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly AppDbContext _ctx;

        public BaseServiceUnitTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;

            var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            // provide new random database name here
            // or parallel test methods will conflict each other
            optionBuilder.UseInMemoryDatabase("test-db");
            optionBuilder.EnableSensitiveDataLogging();
            optionBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
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

            var baseMapper = new BaseMapper<BLL.App.DTO.Exercise, DAL.App.DTO.Exercise>(mapper);

            var uow = new AppUnitOfWork(_ctx, mapper);
            var repo = new ExerciseRepository(_ctx, mapper);
            _baseService = new BaseEntityService<AppUnitOfWork, ExerciseRepository, Exercise, DAL.App.DTO.Exercise>
                (uow, repo, baseMapper);
        }

        private async Task SeedData(int count = 5)
        {
            for (int i = 0; i < count; i++)
            {
                var exercise = new Domain.App.Exercise()
                {
                    Name = $"Exercise {i + 1}",
                    Description = $"Description for exercise {i + 1}",
                    Difficulty = new Domain.App.Difficulty
                    {
                        Name = $"Difficulty {i + 1}"
                    },
                };

                var muscle = new Domain.App.Muscle
                {
                    MedicalName = $"Medical name {i + 1}",
                    EverydayName = $"Everyday name {i + 1}"
                };

                await _ctx.MuscleInExercises.AddAsync(new Domain.App.MuscleInExercise
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
        public async Task Test_GetAllAsync__Returns_Correct_List_And_Type_With_No_Data_Loss(int count)
        {
            // ARRANGE
            await SeedData(count);

            // ACT
            var result = (await _baseService.GetAllAsync()).ToList();

            // ASSERT
            result.Should().NotBeNull();
            result.Should().NotContainNulls();
            result.Count.Should().Be(count);
            result.Should().AllBeOfType(typeof(Exercise));
        }

        [Theory]
        [InlineData(5)]
        public async Task Test_FirstOrDefaultAsync__Returns_Correct_Object_With_Type(int count)
        {
            // ARRANGE
            await SeedData(count);

            // ACT
            var first = await _baseService.FirstOrDefaultAsync(1);
            var last = await _baseService.FirstOrDefaultAsync(count);
            var shouldBeNull = await _baseService.FirstOrDefaultAsync(count + 1);

            // ASSERT
            first.Should().NotBeNull().And.BeOfType<Exercise>();
            last.Should().NotBeNull().And.BeOfType<Exercise>();

            first!.Name.Should().Be("Exercise 1");
            last!.Name.Should().Be($"Exercise {count}");

            shouldBeNull.Should().BeNull();
        }

        [Fact]
        public async Task Test_Add__Returns_Correct_Object_And_Object_Is_Saved()
        {
            var exercise = new Exercise()
            {
                Name = "Add test",
                Description = "Add test desc",
                Difficulty = new Difficulty
                {
                    Name = "Add test difficulty"
                }
            };

            var result = _baseService.Add(exercise);
            await _ctx.SaveChangesAsync();

            result.Should().NotBeNull();
            result.Name.Should().Be("Add test");

            var entityInDb = await _ctx.Exercises.AsNoTracking().FirstOrDefaultAsync(x => x.Id == result.Id);
            entityInDb.Should().NotBeNull();
            entityInDb.Name.Should().Be("Add test");
        }

        [Fact]
        public async Task Test_Update__Returns_Correct_Entity()
        {
            await SeedData();
            var tobeUpdated = await _baseService.FirstOrDefaultAsync(1);
            tobeUpdated!.Name = "Updated"; 
            var updated = _baseService.Update(new Exercise
            {
                Name = "Updated",
                DifficultyId = 2,
                Description = "Updated description",
                Id = 6
            });
            
            updated.Name.Should().Be("Updated");
        }

        [Fact]
        public async Task Test_Update__Returns_Correct_Error_When_Wrong_Id()
        {
            var res = _baseService.Update(new Exercise
            {
                Name = "Updated",
                Difficulty = new Difficulty
                {
                    Id = 1,
                    Name = "Difficulty 1"
                },
                Description = "Updated description",
                Id = 1000
            });

            await Assert.ThrowsAnyAsync<DbUpdateConcurrencyException>(() => _ctx.SaveChangesAsync());
        }

         [Fact]
         public async Task? Test_RemoveAsync__Throws_If_Bad_Id()
         {
             await SeedData();

             await Assert.ThrowsAnyAsync<System.NullReferenceException>(() =>_baseService.RemoveAsync(6));
         }
        

        [Fact]
        public async Task Test_ExistsAsync__Returns_Correct_Boolean()
        {
            await SeedData();

            var truthy = await _baseService.ExistsAsync(1);
            var falsy = await _baseService.ExistsAsync(6);

            truthy.Should().Be(true);
            falsy.Should().Be(falsy);
        }

        [Fact]
        public async Task Test_GetUpdatedEntityAfterSaveChanges__Returns_Correct_Entity()
        {
            var exercise = new Exercise()
            {
                Name = "Add test",
                Description = "Add test desc",
                Difficulty = new Difficulty
                {
                    Name = "Add test difficulty"
                }
            };

            _baseService.Add(exercise);
            await _ctx.SaveChangesAsync();
            var result = _baseService.GetUpdatedEntityAfterSaveChanges(exercise);

            result.Id.Should().NotBe(0);
            result.Id.Should().Be(1);
        }
    }
}