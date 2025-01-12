using WebshopApi.Models;

namespace WebshopApi.Services;

public interface IOrderLineService
{
    IEnumerable<OrderLine> GetOrderLinesByOrderId(int orderId);
    OrderLine? GetOrderLineById(int id);
    void AddOrderLine(OrderLine orderLine);
    void UpdateOrderLine(OrderLine orderLine);
    void DeleteOrderLine(int id);
}