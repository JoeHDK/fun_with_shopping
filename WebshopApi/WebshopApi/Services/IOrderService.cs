using WebshopApi.Models;

namespace WebshopApi.Services;

public interface IOrderService
{
    IEnumerable<Order> GetOrdersBySessionId(string sessionId);
    Order? GetOrderById(int id);
    void CreateOrder(string sessionId);
}