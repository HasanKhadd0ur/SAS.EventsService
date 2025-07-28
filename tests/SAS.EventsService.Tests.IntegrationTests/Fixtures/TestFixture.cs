using Microsoft.Extensions.DependencyInjection;
using SAS.EventsService.Infrastructure.Persistence.AppDataContext;
using SAS.EventsService.Tests.IntegrationTests.Fixtures;

namespace SAS.EventsService.Tests.IntegrationTests.Events
{
    public class TestFixture : IDisposable
    {
        public IServiceProvider Services { get; }
        public AppDbContext DbContext { get; }

        public TestFixture()
        {
            Services = TestStartup.Initialize();
            DbContext = Services.GetRequiredService<AppDbContext>();
            DbContext.Database.EnsureDeleted();
            DbContext.Database.EnsureCreated();
            TestSeeder.Seed(DbContext);
        }

        public void Dispose()
        {
            DbContext.Database.EnsureDeleted();
            DbContext.Dispose();
            if (Services is IDisposable d) d.Dispose();
        }
    }
}
