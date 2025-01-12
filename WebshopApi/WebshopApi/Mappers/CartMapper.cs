using WebshopApi.DTOs;
using WebshopApi.Models;

namespace WebshopApi.Mappers;

public static class CartMapper
{
    private static CartDto ToDto(Cart cart)
    {
        return new CartDto
        {
            Id = cart.Id,
            ProductId = cart.ProductId,
            ProductName = cart.Product.Name,
            Quantity = cart.Quantity,
            Price = cart.Product.Price * cart.Quantity * (1 - cart.Product.Discount / 100)
        };
    }

    public static IEnumerable<CartDto> ToDtoList(IEnumerable<Cart> carts)
    {
        return carts.Select(ToDto);
    }
}