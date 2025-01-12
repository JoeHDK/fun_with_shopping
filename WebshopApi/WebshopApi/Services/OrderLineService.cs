using WebshopApi.Models;
using WebshopApi.Repositories;

namespace WebshopApi.Services;

public class OrderLineService(IOrderLineRepository repository) : IOrderLineService
{
    public IEnumerable<OrderLine> GetOrderLinesByOrderId(int orderId) =>
        repository.GetAllByOrderId(orderId);

    public OrderLine? GetOrderLineById(int id) => repository.GetById(id);

    public void AddOrderLine(OrderLine orderLine)
    {
        repository.Add(orderLine);
        repository.SaveChanges();
    }

    public void UpdateOrderLine(OrderLine orderLine)
    {
        repository.Update(orderLine);
        repository.SaveChanges();
    }

    public void DeleteOrderLine(int id)
    {
        var orderLine = repository.GetById(id);
        if (orderLine == null) return;

        repository.Delete(orderLine);
        repository.SaveChanges();
    }
}