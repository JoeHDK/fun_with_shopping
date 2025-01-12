using WebshopApi.Data;
using WebshopApi.Models;

namespace WebshopApi.Repositories;

public class OrderLineRepository(WebshopDbContext context) : IOrderLineRepository
{
    public IEnumerable<OrderLine> GetAllByOrderId(int orderId) =>
        context.OrderLines.Where(ol => ol.OrderId == orderId).ToList();

    public OrderLine? GetById(int id) => context.OrderLines.Find(id);

    public void Add(OrderLine orderLine)
    {
        context.OrderLines.Add(orderLine);
    }

    public void Update(OrderLine orderLine)
    {
        context.OrderLines.Update(orderLine);
    }

    public void Delete(OrderLine orderLine)
    {
        context.OrderLines.Remove(orderLine);
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }
}