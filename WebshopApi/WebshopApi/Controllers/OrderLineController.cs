using Microsoft.AspNetCore.Mvc;
using WebshopApi.Models;
using WebshopApi.Services;

namespace WebshopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = "Order line")]
public class OrderLineController(IOrderLineService orderLineService) : ControllerBase
{
    [HttpGet("order/{orderId:int}")]
    public IActionResult GetOrderLinesByOrderId(int orderId)
    {
        var orderLines = orderLineService.GetOrderLinesByOrderId(orderId);
        return Ok(orderLines);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetOrderLineById(int id)
    {
        var orderLine = orderLineService.GetOrderLineById(id);
        if (orderLine == null) return NotFound();

        return Ok(orderLine);
    }

    [HttpPost]
    public IActionResult AddOrderLine(OrderLine orderLine)
    {
        orderLineService.AddOrderLine(orderLine);
        return CreatedAtAction(nameof(GetOrderLineById), new { id = orderLine.Id }, orderLine);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateOrderLine(int id, OrderLine updatedOrderLine)
    {
        var existingOrderLine = orderLineService.GetOrderLineById(id);
        if (existingOrderLine == null) return NotFound();

        updatedOrderLine.Id = id;
        orderLineService.UpdateOrderLine(updatedOrderLine);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteOrderLine(int id)
    {
        var existingOrderLine = orderLineService.GetOrderLineById(id);
        if (existingOrderLine == null) return NotFound();

        orderLineService.DeleteOrderLine(id);
        return NoContent();
    }
}