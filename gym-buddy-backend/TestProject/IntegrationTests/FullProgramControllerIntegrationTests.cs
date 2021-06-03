using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BLL.App.DTO;
using DAL.App.EF;
using Domain.App;
using Domain.App.Identity;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using PublicAPI.DTO.v1.Account;
using TestProject.Helpers;
using WebApp;
using Xunit;
using Xunit.Abstractions;
using FullProgram = BLL.App.DTO.FullProgram;
using UserProgram = BLL.App.DTO.UserProgram;

namespace TestProject.IntegrationTests
{
    public class FullProgramControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<WebApp.Startup> _factory;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly AppDbContext _ctx;

        public FullProgramControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory,
            ITestOutputHelper testOutputHelper)
        {
            _factory = factory;
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

            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
                {
                    // dont follow redirects
                    AllowAutoRedirect = false
                }
            );
        }

        [Fact]
        public async Task Test_FullProgram_Selection_For_New_User()
        {
            await CheckProgramsExist();

            await UnRegisteredUserTriesToChooseProgram();

            await RegisterUser();

            var token = await LogInUser();

            await CheckIfUserHasPrograms(token, 0);

            await UserChoosesPrograms(token);

            await CheckIfUserHasPrograms(token, 1);

            await UserChoosesPrograms(token, 3);

            await CheckIfUserHasPrograms(token, 4);
        }

        private async Task UserChoosesPrograms(string token, int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"/api/v1/UserPrograms/{i}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _client.SendAsync(request);
                response.EnsureSuccessStatusCode();
            }
        }

        private async Task CheckIfUserHasPrograms(string token, int userProgramCount)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/v1/UserPrograms/All");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.SendAsync(request);
            var programs =
                JsonHelper.DeserializeWithWebDefaults<List<UserProgram>>(await response.Content.ReadAsStringAsync());

            programs!.Count.Should().Be(userProgramCount);
        }

        private async Task<string> LogInUser()
        {
            var logInDto = new Login
            {
                Email = "test@ttu.ee",
                Password = "Foobar1."
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/v1/account/login")
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(logInDto),
                    Encoding.UTF8,
                    "application/json"),
            };
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var jwtResponse = JsonHelper.DeserializeWithWebDefaults<JwtResponse>(content);
            jwtResponse!.Token.Should().NotBeNullOrEmpty();
            return jwtResponse.Token;
        }

        private async Task RegisterUser()
        {
            var registerDto = new Register
            {
                Email = "test@ttu.ee",
                Firstname = "Test firstname",
                Lastname = "Test lastname",
                Password = "Foobar1."
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/v1/account/register")
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(registerDto),
                    Encoding.UTF8,
                    "application/json"),
            };

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        private async Task CheckProgramsExist()
        {
            var uri = "/api/v1/FullPrograms";

            var getResponse = await _client.GetAsync(uri);

            var programs =
                JsonHelper.DeserializeWithWebDefaults<List<FullProgram>>(await getResponse.Content.ReadAsStringAsync());

            programs!.Count.Should().Be(5);
        }

        private async Task UnRegisteredUserTriesToChooseProgram()
        {
            var uri = "/api/v1/UserPrograms/" + "1";

            var getResponse = await _client.PostAsync(uri, new StringContent(""));

            Assert.Equal(401, (int) getResponse.StatusCode);
        }
    }
}