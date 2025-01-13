using Microsoft.AspNetCore.Mvc;
using WebshopApi.DTOs;
using WebshopApi.Mappers;
using WebshopApi.Models;
using WebshopApi.Services;

namespace WebshopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = "Cart")]
public class CartController(ICartService cartService) : ControllerBase
{
    // Get all cart items for a session
    [HttpGet("{sessionId}")]
    public IActionResult GetCart(string sessionId)
    {
        var cartItems = cartService.GetCartItems(sessionId);
        var cartDtos = CartMapper.ToDtoList(cartItems);
        return Ok(cartDtos);
    }

    // Add an item to the cart
    [HttpPost]
    public IActionResult AddToCart([FromBody] CartItemDto cartItem)
    {
        if (!ModelState.IsValid)
        {
            Console.WriteLine("Validation failed for CartItemDto");
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"Validation Error: {error.ErrorMessage}");
            }
            return BadRequest(ModelState);
        }

        try
        {
            Console.WriteLine($"Adding productId {cartItem.ProductId} to cart for sessionId {cartItem.SessionId} with quantity {cartItem.Quantity}");

            // Call the service to handle the cart addition
            cartService.AddToCart(cartItem.ProductId, cartItem.Quantity, cartItem.SessionId);

            return Ok(new { Message = "Item added to cart successfully." });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while adding to cart: {ex.Message}");
            return StatusCode(500, "An error occurred while adding the item to the cart.");
        }
    }

    // Remove an item from the cart
    [HttpDelete("{id:int}")]
    public IActionResult RemoveFromCart(int id)
    {
        cartService.RemoveFromCart(id);
        return NoContent();
    }
}