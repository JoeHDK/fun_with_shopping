using WebshopApi.DTOs;
using WebshopApi.Models;

namespace WebshopApi.Mappers;

public static class OrderMapper
{
    public static OrderDto ToDto(Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            OrderDate = order.OrderDate,
            TotalPrice = order.TotalPrice,
            OrderLines = order.OrderLines.Select(ol => new OrderLineDto
            {
                ProductId = ol.ProductId,
                ProductName = ol.Product.Name,
                Quantity = ol.Quantity,
                Price = ol.Price
            }).ToList()
        };
    }
}