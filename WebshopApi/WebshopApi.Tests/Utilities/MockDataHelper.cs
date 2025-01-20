using WebshopApi.Models;

namespace WebshopApi.Tests.Utilities
{
    public static class MockDataHelper
    {
        public static Cart GetMockCart()
        {
            return new Cart
            {
                Id = 1,
                ProductId = 1,
                Quantity = 2,
                SessionId = "test-session-id",
                Product = new Product { Id = 1, Name = "Test Product", Price = 100, Discount = 10 }
            };
        }

        public static IEnumerable<Cart> GetMockCartList()
        {
            return new List<Cart>
            {
                GetMockCart(),
                new Cart
                {
                    Id = 2,
                    ProductId = 2,
                    Quantity = 1,
                    SessionId = "test-session-id",
                    Product = new Product { Id = 2, Name = "Another Product", Price = 200, Discount = 0 }
                }
            };
        }
    }
}