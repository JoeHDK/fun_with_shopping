using WebshopApi.Models;
using WebshopApi.Repositories;

namespace WebshopApi.Services;

public class OrderService(IOrderRepository orderRepository, ICartRepository cartRepository)
    : IOrderService
{
    public IEnumerable<Order> GetOrdersBySessionId(string sessionId) =>
        orderRepository.GetAllBySessionId(sessionId);

    public Order? GetOrderById(int id) => orderRepository.GetById(id);

    public void CreateOrder(string sessionId)
    {
        var cartItems = cartRepository.GetAllBySessionId(sessionId);

        var enumerable = cartItems as Cart[] ?? cartItems.ToArray();
        if (!enumerable.Any())
        {
            throw new InvalidOperationException("Cart is empty.");
        }

        decimal totalPrice = 0;

        foreach (var item in enumerable)
        {
            var itemPrice = item.Product.Price * item.Quantity;

            // Apply bulk discount if 5 or more of the same product
            if (item.Quantity >= 5)
            {
                itemPrice *= (1 - item.Product.Discount / 100);
            }

            totalPrice += itemPrice;
        }

        // Apply price threshold discount if total exceeds 5000
        if (totalPrice > 5000)
        {
            totalPrice *= 0.95m; // 5% discount
        }

        var newOrder = new Order
        {
            SessionId = sessionId,
            OrderDate = DateTime.Now,
            TotalPrice = totalPrice,
            OrderLines = enumerable.Select(c => new OrderLine
            {
                ProductId = c.ProductId,
                Quantity = c.Quantity,
                Price = c.Product.Price * (c.Quantity >= 5 ? (1 - c.Product.Discount / 100) : 1)
            }).ToList()
        };

        orderRepository.Add(newOrder);

        // Clear the cart
        foreach (var item in enumerable)
        {
            cartRepository.Delete(item);
        }

        orderRepository.SaveChanges();
        cartRepository.SaveChanges();
    }

}