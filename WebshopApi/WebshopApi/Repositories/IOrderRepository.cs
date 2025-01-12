using WebshopApi.Models;

namespace WebshopApi.Repositories;

public interface IOrderRepository
{
    IEnumerable<Order> GetAllBySessionId(string sessionId);
    Order? GetById(int id);
    void Add(Order order);
    void Update(Order order);
    void Delete(Order order);
    void SaveChanges();
}