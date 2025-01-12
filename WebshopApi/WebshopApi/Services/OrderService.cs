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
        if (enumerable.Length == 0)
        {
            throw new InvalidOperationException("Cart is empty.");
        }

        var newOrder = new Order
        {
            SessionId = sessionId,
            OrderDate = DateTime.Now,
            TotalPrice = enumerable.Sum(c => c.Quantity * c.Product.Price * (1 - c.Product.Discount / 100)),
            OrderLines = enumerable.Select(c => new OrderLine
            {
                ProductId = c.ProductId,
                Quantity = c.Quantity,
                Price = c.Product.Price * (1 - c.Product.Discount / 100)
            }).ToList()
        };

        orderRepository.Add(newOrder);

        foreach (var cartItem in enumerable)
        {
            cartRepository.Delete(cartItem);
        }

        orderRepository.SaveChanges();
        cartRepository.SaveChanges();
    }
}