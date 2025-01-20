using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebshopApi.Tests.Utilities;
using Xunit;

namespace WebshopApi.Tests.IntegrationTests
{
    public class CartApiIntegrationTests
    {
        private readonly HttpClient _client = HttpClientFactoryHelper.GetTestClient();

        [Fact]
        public async Task AddToCart_ValidInput_ReturnsNoContent()
        {
            // Arrange
            var cartItem = new
            {
                ProductId = 1,
                Quantity = 2,
                SessionId = "test-session-id"
            };
            var content = new StringContent(JsonSerializer.Serialize(cartItem), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/cart", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}