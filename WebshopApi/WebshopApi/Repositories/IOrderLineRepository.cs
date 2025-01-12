using WebshopApi.Models;

namespace WebshopApi.Repositories;

public interface IOrderLineRepository
{
    IEnumerable<OrderLine> GetAllByOrderId(int orderId);
    OrderLine? GetById(int id);
    void Add(OrderLine orderLine);
    void Update(OrderLine orderLine);
    void Delete(OrderLine orderLine);
    void SaveChanges();
}