using WebshopApi.DTOs;
using WebshopApi.Models;

namespace WebshopApi.Mappers;

public static class CartMapper
{
    private static CartDto ToDto(Cart cart)
    {
        if (cart.Product == null)
        {
            throw new Exception($"Cart item with ID {cart.Id} has no associated product.");
        }

        // Apply discounts
        var basePrice = cart.Product.Price * cart.Quantity;
        var discountedPrice = basePrice;

        // Apply item-level discount (if quantity >= 5)
        if (cart.Quantity >= 5)
        {
            discountedPrice = basePrice * (1 - cart.Product.Discount / 100);
        }

        return new CartDto
        {
            Id = cart.Id,
            ProductId = cart.ProductId,
            ProductName = cart.Product.Name,
            Quantity = cart.Quantity,
            Price = basePrice, 
            TotalPrice = discountedPrice, 
            Discount = cart.Quantity >= 5 ? cart.Product.Discount : 0,
        };
    }

    public static IEnumerable<CartDto> ToDtoList(IEnumerable<Cart> carts)
    {
        return carts.Select(ToDto);
    }
}