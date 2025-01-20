using Microsoft.AspNetCore.Mvc;
using Moq;
using WebshopApi.Controllers;
using WebshopApi.Services;
using WebshopApi.Tests.Utilities;
using Xunit;

namespace WebshopApi.Tests.UnitTests.Controllers
{
    public class CartControllerTests
    {
        private readonly Mock<ICartService> _cartServiceMock;
        private readonly CartController _cartController;

        public CartControllerTests()
        {
            _cartServiceMock = new Mock<ICartService>();
            _cartController = new CartController(_cartServiceMock.Object);
        }

        [Fact]
        public void GetCart_ValidSessionId_ReturnsOk()
        {
            // Arrange
            const string sessionId = "test-session-id";
            _cartServiceMock.Setup(s => s.GetCartBySessionId(sessionId))
                .Returns(MockDataHelper.GetMockCartList());

            // Act
            var result = _cartController.GetCart(sessionId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }
    }
}