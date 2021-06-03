using System.Linq;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TestProject
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // find the dbcontext
                var descriptor = services
                    .SingleOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<AppDbContext>)
                    );
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<AppDbContext>(options =>
                {
                    // do we need unique db?
                    options.UseInMemoryDatabase("testdb");
                });

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<AppDbContext>();

                db.Database.EnsureCreated();

                SeedData(5, db);
            });
        }
        
        private void SeedData(int count, AppDbContext ctx)
        {
            for (int i = 0; i < count; i++)
            {
                ctx.FullPrograms.Add(new Domain.App.FullProgram
                {
                    Name = $"Seeded program {i}",
                    Description = $"Seeded desc {i}",
                    Goal = $"Seeded goal {i}"
                });
            }

            ctx.SaveChanges();

        }
    }
}