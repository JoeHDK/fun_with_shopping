using System.Linq;
using WebshopApi.Repositories;
using WebshopApi.Tests.Utilities;
using Xunit;

namespace WebshopApi.Tests.UnitTests.Repositories;
public class CartRepositoryTests : TestBase
{
    private readonly CartRepository _cartRepository;

    public CartRepositoryTests()
    {
        _cartRepository = new CartRepository(DbContext);
    }

    [Fact]
    public void GetAllBySessionId_ValidSessionId_ReturnsCarts()
    {
        // Arrange
        const string sessionId = "test-session-id";
        DbContext.Cart.AddRange(MockDataHelper.GetMockCartList());
        DbContext.SaveChanges();

        // Act
        var carts = _cartRepository.GetAllBySessionId(sessionId);

        // Assert
        Assert.Equal(2, carts.Count());
    }
}