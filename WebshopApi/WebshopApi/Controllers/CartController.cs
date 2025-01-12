using Microsoft.AspNetCore.Mvc;
using WebshopApi.DTOs;
using WebshopApi.Mappers;
using WebshopApi.Models;
using WebshopApi.Services;

namespace WebshopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    public IActionResult AddToCart([FromBody] Cart cartItem)
    {
        cartService.AddToCart(cartItem);
        return NoContent();
    }

    // Remove an item from the cart
    [HttpDelete("{id:int}")]
    public IActionResult RemoveFromCart(int id)
    {
        cartService.RemoveFromCart(id);
        return NoContent();
    }
}