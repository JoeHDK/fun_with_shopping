namespace WebshopApi.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderLineDto> OrderLines { get; set; } = [];
}