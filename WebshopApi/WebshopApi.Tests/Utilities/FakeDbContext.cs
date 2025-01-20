using Microsoft.EntityFrameworkCore;
using WebshopApi.Data;

namespace WebshopApi.Tests.Utilities
{
    public static class FakeDbContext
    {
        public static WebshopDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<WebshopDbContext>()
                .UseInMemoryDatabase(databaseName: "WebshopTestDb")
                .Options;

            var context = new WebshopDbContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }
    }
}