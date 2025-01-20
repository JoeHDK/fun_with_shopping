using Moq;
using WebshopApi.DTOs;
using WebshopApi.Models;
using WebshopApi.Repositories;
using WebshopApi.Services;
using Xunit;

namespace WebshopApi.Tests.UnitTests.Services;

public class CartServiceTests
{
    private readonly Mock<ICartRepository> _cartRepositoryMock;
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly CartService _cartService;

    public CartServiceTests()
    {
        _cartRepositoryMock = new Mock<ICartRepository>();
        _productRepositoryMock = new Mock<IProductRepository>();
        _cartService = new CartService(_cartRepositoryMock.Object, _productRepositoryMock.Object);
    }

    [Fact]
    public void AddToCart_ValidInput_AddsItemSuccessfully()
    {
        // Arrange
        const int productId = 1;
        const int quantity = 2;
        const string sessionId = "test-session-id";

        var mockProduct = new Product
        {
            Id = productId,
            Name = "Test Product",
            Price = 100m,
            Discount = 0
        };

        _productRepositoryMock.Setup(repo => repo.GetById(productId)).Returns(mockProduct);
        _productRepositoryMock.Setup(repo => repo.ProductExists(productId)).Returns(true);

        // Act
        _cartService.AddToCart(productId, quantity, sessionId);

        // Assert
        _cartRepositoryMock.Verify(repo => repo.Add(It.Is<Cart>(
            cart => cart.ProductId == productId && 
                    cart.Quantity == quantity && 
                    cart.SessionId == sessionId)), Times.Once);
        _cartRepositoryMock.Verify(repo => repo.SaveChanges(), Times.Once);
    }

    [Fact]
    public void AddToCart_InvalidProduct_ThrowsException()
    {
        // Arrange
        const int productId = 99; // Non-existent product ID
        const int quantity = 2;
        const string sessionId = "test-session-id";

        _productRepositoryMock.Setup(repo => repo.ProductExists(productId)).Returns(false);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            _cartService.AddToCart(productId, quantity, sessionId));

        Assert.Equal("Invalid product ID.", exception.Message);
        _cartRepositoryMock.Verify(repo => repo.Add(It.IsAny<Cart>()), Times.Never);
    }
}