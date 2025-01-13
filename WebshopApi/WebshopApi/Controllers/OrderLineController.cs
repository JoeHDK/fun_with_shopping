using Microsoft.AspNetCore.Mvc;
using WebshopApi.DTOs;
using WebshopApi.Mappers;
using WebshopApi.Models;
using WebshopApi.Services;

namespace WebshopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = "OrderLine")]
public class OrderLineController(IOrderLineService orderLineService) : ControllerBase
{
    // Get all order lines for a specific order
    [HttpGet("order/{orderId:int}")]
    public IActionResult GetOrderLinesByOrderId(int orderId)
    {
        var orderLines = orderLineService.GetOrderLinesByOrderId(orderId);
        var orderLineDtos = orderLines.Select(ol => new OrderLineDto
        {
            ProductId = ol.ProductId,
            ProductName = ol.Product.Name,
            Quantity = ol.Quantity,
            Price = ol.Price
        });
        return Ok(orderLineDtos);
    }

    // Get a single order line by ID
    [HttpGet("{id:int}")]
    public IActionResult GetOrderLineById(int id)
    {
        var orderLine = orderLineService.GetOrderLineById(id);
        if (orderLine == null) return NotFound();

        var orderLineDto = new OrderLineDto
        {
            ProductId = orderLine.ProductId,
            ProductName = orderLine.Product.Name,
            Quantity = orderLine.Quantity,
            Price = orderLine.Price
        };

        return Ok(orderLineDto);
    }

    // Add a new order line
    [HttpPost]
    public IActionResult AddOrderLine([FromBody] OrderLine orderLine)
    {
        orderLineService.AddOrderLine(orderLine);
        return CreatedAtAction(nameof(GetOrderLineById), new { id = orderLine.Id }, orderLine);
    }

    // Update an existing order line
    [HttpPut("{id:int}")]
    public IActionResult UpdateOrderLine(int id, [FromBody] OrderLine updatedOrderLine)
    {
        var existingOrderLine = orderLineService.GetOrderLineById(id);
        if (existingOrderLine == null) return NotFound();

        updatedOrderLine.Id = id;
        orderLineService.UpdateOrderLine(updatedOrderLine);
        return NoContent();
    }

    // Delete an order line
    [HttpDelete("{id:int}")]
    public IActionResult DeleteOrderLine(int id)
    {
        var existingOrderLine = orderLineService.GetOrderLineById(id);
        if (existingOrderLine == null) return NotFound();

        orderLineService.DeleteOrderLine(id);
        return NoContent();
    }
}