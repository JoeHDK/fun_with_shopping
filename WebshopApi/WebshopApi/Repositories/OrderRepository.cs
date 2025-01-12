using WebshopApi.Data;
using WebshopApi.Models;

namespace WebshopApi.Repositories;

public class OrderRepository(WebshopDbContext context) : IOrderRepository
{
    public IEnumerable<Order> GetAllBySessionId(string sessionId) =>
        context.Orders.Where(o => o.SessionId == sessionId).ToList();

    public Order? GetById(int id) => context.Orders.Find(id);

    public void Add(Order order)
    {
        context.Orders.Add(order);
    }

    public void Update(Order order)
    {
        context.Orders.Update(order);
    }

    public void Delete(Order order)
    {
        context.Orders.Remove(order);
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }
}