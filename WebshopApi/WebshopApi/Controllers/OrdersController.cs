using Microsoft.AspNetCore.Mvc;
using WebshopApi.Mappers;
using WebshopApi.Services;

namespace WebshopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    // Get details of a specific order
    [HttpGet("{id:int}")]
    public IActionResult GetOrder(int id)
    {
        var order = orderService.GetOrderById(id);
        if (order == null) return NotFound();

        var orderDto = OrderMapper.ToDto(order);
        return Ok(orderDto);
    }

    // Create a new order for a session
    [HttpPost("{sessionId}")]
    public IActionResult CreateOrder(string sessionId)
    {
        try
        {
            orderService.CreateOrder(sessionId);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}