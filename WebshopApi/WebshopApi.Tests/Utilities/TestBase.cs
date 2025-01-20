using WebshopApi.Data;

namespace WebshopApi.Tests.Utilities
{
    public abstract class TestBase
    {
        protected WebshopDbContext DbContext { get; } = FakeDbContext.GetInMemoryDbContext();
    }
}